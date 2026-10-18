using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyCom.Class.YouTube
{
    public enum BrowserSource
    {
        None,
        Chrome,
        Firefox,
        Edge,
        Brave,
        Opera,
        Vivaldi,
        Chromium
    }

    public sealed class YtDlpResult
    {
        public bool Success { get; set; }

        public int ExitCode { get; set; }

        public string StandardOutput { get; set; } =
            string.Empty;

        public string StandardError { get; set; } =
            string.Empty;

        public bool WasBotDetectionError { get; set; }

        public bool WasPoTokenError { get; set; }

        public string? DownloadedFilePath { get; set; }
    }

    public sealed class YtDlpOptions
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public string YtDlpExecutablePath { get; set; } = Path.Combine(BaseDirectory, "yt-dlp.exe");

        public string OutputDirectory { get; set; } = Path.Combine(BaseDirectory, "Downloads");

        public BrowserSource CookieBrowser { get; set; } = BrowserSource.None;

        public string? BrowserProfile { get; set; }

        public string? CookiesFilePath { get; set; }

        public bool AutoUpdateBeforeRun { get; set; } = false;

        public TimeSpan UpdateCheckInterval { get; set; } = TimeSpan.FromHours(24);

        public bool EnablePoTokenProvider { get; set; } = false;

        public string PoTokenProviderDirectory { get; set; } = "bgutil-provider";

        public string PoTokenServerBaseUrl { get; set; } = "http://127.0.0.1:4416";

        public string NodeExecutablePath { get; set; } = "node";

        public string UserAgent { get; set; } =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
            "AppleWebKit/537.36 (KHTML, like Gecko) " +
            "Chrome/126.0.0.0 Safari/537.36";

        public double SleepRequestsSeconds { get; set; } =
            0;

        public int MaxRetries { get; set; } =
            2;

        public TimeSpan RetryDelay { get; set; } =
            TimeSpan.FromSeconds(5);

        public string? RateLimit { get; set; }

        public TimeSpan ProcessTimeout { get; set; } =
            TimeSpan.FromMinutes(60);
    }

    public sealed class YtDlpManager : IDisposable
    {
        private readonly YtDlpOptions _options;

        public YtDlpManager(YtDlpOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            Directory.CreateDirectory(_options.OutputDirectory);
            YoutubeLogger.Info("YtDlpManager ساخته شد.");
            YoutubeLogger.Debug($"yt-dlp: {_options.YtDlpExecutablePath}");
            YoutubeLogger.Debug($"Output: {_options.OutputDirectory}");
        }

        public async Task<YtDlpResult> DownloadAsync(string videoUrl, string movieName, int year, string extraArguments = "", CancellationToken cancellationToken = default)
        {

            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                throw new ArgumentException("لینک ویدیو خالی است.", nameof(videoUrl));
            }

            if (!File.Exists(_options.YtDlpExecutablePath))
            {
                throw new FileNotFoundException("yt-dlp.exe پیدا نشد.", _options.YtDlpExecutablePath);
            }

            string beforeFiles = GetDownloadFiles();

            string authentication = BuildAuthenticationArguments();

            string outputTemplate = Path.Combine(_options.OutputDirectory, $"Trl={movieName + "_" + year}.mp4");
            //string outputTemplate = Path.Combine(_options.OutputDirectory, "%(title)s [%(id)s].%(ext)s");
            string proxy = "--proxy socks5h://127.0.0.1:10808";
            string arguments =
                $"{proxy} " +
                $"{authentication} " +
                $"--no-playlist " +
                $"--newline " +
                $"--retries 5 " +
                $"--fragment-retries 5 " +
                $"--merge-output-format mp4 " +
                $"--format \"bestvideo[height<=480]+bestaudio/best\" " +  // ← اضافه شد
                $"-o \"{outputTemplate}\" " +
                $"{extraArguments} " +
                $"\"{videoUrl}\"";

            //string arguments =
            //    $"{authentication} " +
            //    $"--no-playlist " +
            //    $"--newline " +
            //    $"--retries 5 " +
            //    $"--fragment-retries 5 " +
            //    $"--merge-output-format mp4 " +
            //    $"-o \"{outputTemplate}\" " +
            //    $"{extraArguments} " +
            //    $"\"{videoUrl}\"";

            YoutubeLogger.Info(
                "شروع دانلود Trailer.");

            YoutubeLogger.Debug(
                $"URL: {videoUrl}");

            YoutubeLogger.Debug(
                $"Arguments: {arguments}");

            YtDlpResult result =
                await RunWithRetryAsync(
                    arguments,
                    cancellationToken);

            if (result.Success)
            {
                result.DownloadedFilePath = FindNewDownloadedFile(beforeFiles);

                if (string.IsNullOrWhiteSpace(
                        result.DownloadedFilePath))
                {
                    YoutubeLogger.Warning(
                        "yt-dlp موفق بود اما فایل خروجی پیدا نشد.");

                    result.Success = false;

                    result.StandardError +=
                        Environment.NewLine +
                        "فایل خروجی دانلود پیدا نشد.";
                }
                else
                {
                    YoutubeLogger.Info(
                        $"دانلود موفق: {result.DownloadedFilePath}");
                }
            }

            return result;
        }

        private async Task<YtDlpResult> RunWithRetryAsync(
            string arguments,
            CancellationToken cancellationToken)
        {
            YtDlpResult result =
                new();

            for (int attempt = 1;
                 attempt <= _options.MaxRetries;
                 attempt++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                YoutubeLogger.Info(
                    $"تلاش دانلود {attempt} از {_options.MaxRetries}");

                result =
                    await RunYtDlpAsync(
                        arguments,
                        cancellationToken);

                if (result.Success)
                    return result;

                if (attempt <
                    _options.MaxRetries &&
                    (result.WasBotDetectionError ||
                     result.WasPoTokenError))
                {
                    YoutubeLogger.Warning(
                        "خطای Bot/PO Token. " +
                        $"تلاش مجدد پس از {_options.RetryDelay}.");

                    await Task.Delay(
                        _options.RetryDelay,
                        cancellationToken);
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private async Task<YtDlpResult> RunYtDlpAsync(
            string arguments,
            CancellationToken cancellationToken)
        {
            try
            {
                RawProcessResult raw =
                    await RunProcessAsync(
                        _options.YtDlpExecutablePath,
                        arguments,
                        _options.ProcessTimeout,
                        cancellationToken);

                var result =
                    new YtDlpResult
                    {
                        ExitCode =
                            raw.ExitCode,

                        StandardOutput =
                            raw.StandardOutput,

                        StandardError =
                            raw.StandardError,

                        Success =
                            raw.ExitCode == 0
                    };

                result.WasBotDetectionError =
                    ContainsBotDetection(
                        raw.StandardError);

                result.WasPoTokenError =
                    ContainsPoTokenError(
                        raw.StandardError);

                if (!result.Success)
                {
                    YoutubeLogger.Error(
                        $"yt-dlp با ExitCode={result.ExitCode} پایان یافت.");

                    YoutubeLogger.Error(
                        raw.StandardError);
                }

                return result;
            }
            catch (Exception ex)
            {
                YoutubeLogger.Error(
                    "خطا در اجرای yt-dlp.",
                    ex);

                return new YtDlpResult
                {
                    Success = false,
                    ExitCode = -1,
                    StandardError = ex.ToString()
                };
            }
        }

        private string BuildAuthenticationArguments()
        {
            var args =
                new StringBuilder();

            if (!string.IsNullOrWhiteSpace(
                    _options.CookiesFilePath))
            {
                if (!File.Exists(
                        _options.CookiesFilePath))
                {
                    throw new FileNotFoundException(
                        "cookies.txt پیدا نشد.",
                        _options.CookiesFilePath);
                }

                args.Append(
                    $"--cookies \"{_options.CookiesFilePath}\" ");
            }
            else if (_options.CookieBrowser !=
                     BrowserSource.None)
            {
                string browser =
                    _options.CookieBrowser
                        .ToString()
                        .ToLowerInvariant();

                string profile =
                    string.IsNullOrWhiteSpace(
                        _options.BrowserProfile)
                        ? string.Empty
                        : $":{_options.BrowserProfile}";

                args.Append($"--cookies-from-browser " + $"\"{browser}{profile}\" ");
            }

            if (!string.IsNullOrWhiteSpace(
                    _options.UserAgent))
            {
                args.Append(
                    $"--user-agent \"{_options.UserAgent}\" ");
            }

            if (!string.IsNullOrWhiteSpace(
                    _options.RateLimit))
            {
                args.Append(
                    $"--limit-rate {_options.RateLimit} ");
            }

            return args.ToString();
        }

        private string GetDownloadFiles()
        {
            if (!Directory.Exists(
                    _options.OutputDirectory))
            {
                return string.Empty;
            }

            return string.Join(
                "|",
                Directory.GetFiles(
                    _options.OutputDirectory));
        }

        private string? FindNewDownloadedFile(
            string beforeFiles)
        {
            string[] oldFiles =
                string.IsNullOrWhiteSpace(
                    beforeFiles)
                    ? Array.Empty<string>()
                    : beforeFiles.Split(
                        '|',
                        StringSplitOptions.RemoveEmptyEntries);

            return Directory
                .GetFiles(
                    _options.OutputDirectory)
                .WhereNotIn(oldFiles)
                .OrderByDescending(
                    File.GetLastWriteTimeUtc)
                .FirstOrDefault();
        }

        private static bool ContainsBotDetection(
            string text)
        {
            return text.Contains(
                       "Sign in to confirm you're not a bot",
                       StringComparison.OrdinalIgnoreCase)
                   ||
                   text.Contains(
                       "confirm you're not a bot",
                       StringComparison.OrdinalIgnoreCase)
                   ||
                   text.Contains(
                       "HTTP Error 429",
                       StringComparison.OrdinalIgnoreCase);
        }

        private static bool ContainsPoTokenError(
            string text)
        {
            return Regex.IsMatch(
                text ?? string.Empty,
                "po[_ -]?token",
                RegexOptions.IgnoreCase);
        }

        private sealed class RawProcessResult
        {
            public int ExitCode { get; set; }

            public string StandardOutput { get; set; } =
                string.Empty;

            public string StandardError { get; set; } =
                string.Empty;
        }

        private static async Task<RawProcessResult>
            RunProcessAsync(
                string fileName,
                string arguments,
                TimeSpan timeout,
                CancellationToken cancellationToken)
        {
            var startInfo =
                new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding =
                        Encoding.UTF8,
                    StandardErrorEncoding =
                        Encoding.UTF8
                };

            using var process =
                new Process
                {
                    StartInfo = startInfo
                };

            process.Start();

            Task<string> stdoutTask =
                process.StandardOutput
                    .ReadToEndAsync();

            Task<string> stderrTask =
                process.StandardError
                    .ReadToEndAsync();

            Task waitTask =
                process.WaitForExitAsync(
                    cancellationToken);

            Task timeoutTask =
                Task.Delay(
                    timeout,
                    cancellationToken);

            Task completed =
                await Task.WhenAny(
                    waitTask,
                    timeoutTask);

            if (completed != waitTask)
            {
                try
                {
                    if (!process.HasExited)
                    {
                        process.Kill(
                            entireProcessTree: true);
                    }
                }
                catch
                {
                }

                throw new TimeoutException(
                    $"اجرای {fileName} بیش از " +
                    $"{timeout} طول کشید.");
            }

            string stdout =
                await stdoutTask;

            string stderr =
                await stderrTask;

            return new RawProcessResult
            {
                ExitCode =
                    process.ExitCode,

                StandardOutput =
                    stdout,

                StandardError =
                    stderr
            };
        }

        public void Dispose()
        {
        }
    }

    internal static class FileEnumerableExtensions
    {
        public static IEnumerable<string>
            WhereNotIn(
                this IEnumerable<string> source,
                string[] excluded)
        {
            var set =
                new HashSet<string>(
                    excluded,
                    StringComparer.OrdinalIgnoreCase);

            return source.Where(
                x => !set.Contains(x));
        }

        private static IEnumerable<string> Where(
            this IEnumerable<string> source,
            Func<string, bool> predicate)
        {
            foreach (string item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }
}