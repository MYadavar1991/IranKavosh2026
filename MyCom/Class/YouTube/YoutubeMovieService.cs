using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MyCom.Class.YouTube
{
    public sealed class YoutubeSearchResult
    {
        public string Title { get; set; } = string.Empty;

        public string Uploader { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public int? DurationSeconds { get; set; }

        public long? ViewCount { get; set; }

        public string? UploadDate { get; set; }

        public double Score { get; set; }
    }


    public sealed class YoutubeMovieService
    {
        private readonly ProxyManager _proxyManager;

        private readonly YtDlpManager _ytDlpManager;

        private readonly string? _cookieFile;

        private static readonly string[] UnwantedWords =
        {
            "reaction",
            "review",
            "breakdown",
            "explained",
            "recap",
            "full movie",
            "fan made",
            "fanmade",
            "concept trailer",
            "parody",
            "short film"
        };

        //private static readonly string[] TrailerWords =
        //{
        //    "trailer",
        //    "teaser",
        //    "official trailer",
        //    "official teaser"
        //};

        public YoutubeMovieService(ProxyManager proxyManager, YtDlpManager ytDlpManager, string? cookieFile = null, string? poToken = null, string? poTokenVisitorData = null)
        {
            _proxyManager = proxyManager ?? throw new ArgumentNullException(nameof(proxyManager));

            _ytDlpManager = ytDlpManager ?? throw new ArgumentNullException(nameof(ytDlpManager));

            _cookieFile = string.IsNullOrWhiteSpace(cookieFile) ? null : cookieFile;

            YoutubeLogger.Info("YoutubeMovieService ساخته شد.");

            YoutubeLogger.Debug($"Cookie File: {_cookieFile}");
        }

        public async Task<List<YoutubeSearchResult>> SearchAsync(string movieName, int year, int count = 10, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(movieName))
            {
                throw new ArgumentException("نام فیلم خالی است.", nameof(movieName));
            }

            string query = $"ytsearch{count}:" + $"{movieName} {year} official trailer";

            YoutubeLogger.Info($"Search Query: {query}");

            string ytDlpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "yt-dlp.exe");

            if (!File.Exists(ytDlpPath))
            {
                throw new FileNotFoundException("yt-dlp.exe پیدا نشد.", ytDlpPath);
            }

            var args = new List<string>();

            args.Add($"--proxy \"{_proxyManager.ProxyUrl}\"");

            if (!string.IsNullOrWhiteSpace(_cookieFile))
            {
                if (!File.Exists(_cookieFile))
                {
                    throw new FileNotFoundException("cookies.txt پیدا نشد.", _cookieFile);
                }

                args.Add($"--cookies \"{_cookieFile}\"");
            }

            args.Add("--dump-json");
            args.Add("--no-download");
            args.Add("--ignore-errors");
            args.Add("--no-warnings");

            string arguments = string.Join(" ", args) + $" \"{query}\"";

            YoutubeLogger.Debug($"Search Arguments: {arguments}");

            var results = new List<YoutubeSearchResult>();

            using var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ytDlpPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.Start();

            Task<string> errorTask = process.StandardError.ReadToEndAsync();

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string? line = await process.StandardOutput.ReadLineAsync();

                if (line == null)
                    break;

                if (string.IsNullOrWhiteSpace(
                        line))
                    continue;

                try
                {
                    using JsonDocument document = JsonDocument.Parse(line);

                    JsonElement root = document.RootElement;

                    string id = GetString(root, "id");

                    if (string.IsNullOrWhiteSpace(id))
                        continue;

                    string url = GetString(root, "webpage_url");

                    if (string.IsNullOrWhiteSpace(url))
                    {
                        url = $"https://www.youtube.com/watch?v={id}";
                    }

                    results.Add(new YoutubeSearchResult
                    {
                        Title = GetString(root, "title"),
                        Uploader = GetString(root, "uploader"),
                        Url = url,
                        DurationSeconds = GetInt(root, "duration"),
                        ViewCount = GetLong(root, "view_count"),
                        UploadDate = GetString(root, "upload_date")
                    });
                }
                catch
                {
                    // خطوط غیر JSON نادیده گرفته می‌شوند.
                }
            }

            await process.WaitForExitAsync(cancellationToken);

            string error = await errorTask;

            if (!string.IsNullOrWhiteSpace(error))
            {
                YoutubeLogger.Warning($"Search Error: {error}");
            }

            return results.Where(x => !string.IsNullOrWhiteSpace(x.Url)).GroupBy(x => x.Url)
                .Select(x =>
                    x.First())
                .ToList();
        }

        public YoutubeSearchResult? SelectBest(List<YoutubeSearchResult> candidates, string movieName, int year)
        {
            if (candidates == null ||
                candidates.Count == 0)
            {
                return null;
            }

            string[] movieWords =
                movieName
                    .ToLowerInvariant()
                    .Split(
                        ' ',
                        StringSplitOptions.RemoveEmptyEntries);

            foreach (YoutubeSearchResult candidate
                     in candidates)
            {
                string title =
                    candidate.Title
                        .ToLowerInvariant();

                string uploader =
                    candidate.Uploader
                        .ToLowerInvariant();

                double score = 0;

                // نام فیلم
                int matchedWords =
                    movieWords.Count(
                        word =>
                            word.Length >= 2 &&
                            title.Contains(
                                word,
                                StringComparison.OrdinalIgnoreCase));

                score +=
                    matchedWords * 25;

                // Trailer
                if (title.Contains(
                        "official trailer"))
                {
                    score += 40;
                }
                else if (title.Contains(
                             "trailer"))
                {
                    score += 25;
                }
                else if (title.Contains(
                             "teaser"))
                {
                    score += 15;
                }

                // سال
                if (title.Contains(
                        year.ToString()))
                {
                    score += 20;
                }

                // کانال‌های رسمی/معتبر
                string[] trusted =
                {
                    "official",
                    "pictures",
                    "studios",
                    "entertainment",
                    "films",
                    "movies",
                    "cinema",
                    "netflix",
                    "marvel",
                    "dc",
                    "warner",
                    "universal",
                    "paramount",
                    "sony",
                    "lionsgate"
                };

                if (trusted.Any(
                        x =>
                            uploader.Contains(
                                x)))
                {
                    score += 30;
                }

                // مدت منطقی Trailer
                if (candidate.DurationSeconds
                    .HasValue)
                {
                    int duration =
                        candidate.DurationSeconds
                            .Value;

                    if (duration is >= 45 and <= 240)
                    {
                        score += 20;
                    }
                    else if (duration > 600)
                    {
                        score -= 100;
                    }
                }

                // حذف نتایج بی‌ربط
                if (UnwantedWords.Any(word => title.Contains(word)))
                {
                    score -= 150;
                }

                // بازدید
                if (candidate.ViewCount is > 0)
                {
                    score += Math.Min(20, Math.Log10(candidate.ViewCount.Value));
                }

                candidate.Score = score;
            }

            return candidates.OrderByDescending(x => x.Score).FirstOrDefault();
        }
        string _movieName = "";
        int _year = 0;
        public async Task<YtDlpResult> DownloadMovieTrailerAsync(string movieName, int year, string extraArguments = "",
            CancellationToken cancellationToken = default)
        {
            _movieName = movieName;
            _year = year;
            YoutubeLogger.Info("========================================");
            YoutubeLogger.Info("شروع عملیات دانلود Trailer.");
            YoutubeLogger.Info($"Movie: {movieName}");
            YoutubeLogger.Info($"Year: {year}");

            try
            {
                bool proxyReady = await _proxyManager.StartAsync(cancellationToken: cancellationToken);

                if (!proxyReady)
                {
                    return new YtDlpResult
                    {
                        Success = false,
                        ExitCode = -1,
                        StandardError = "Proxy آماده نشد."
                    };
                }

                List<YoutubeSearchResult> candidates = await SearchAsync(movieName, year, 10, cancellationToken);

                YoutubeLogger.Info($"تعداد نتایج: {candidates.Count}");
                YoutubeSearchResult? best = SelectBest(candidates, movieName, year);

                if (best == null)
                {
                    YoutubeLogger.Error("نتیجه مناسبی برای فیلم پیدا نشد.");

                    return new YtDlpResult
                    {
                        Success = false,
                        ExitCode = -1,
                        StandardError = "نتیجه مناسب پیدا نشد."
                    };
                }

                YoutubeLogger.Info("بهترین نتیجه انتخاب شد.");
                //YoutubeLogger.Info($"Title: {best.Title}");
                //YoutubeLogger.Info($"Uploader: {best.Uploader}");
                //YoutubeLogger.Info($"Score: {best.Score}");
                //YoutubeLogger.Info($"URL: {best.Url}");

                return await _ytDlpManager.DownloadAsync(best.Url, _movieName, _year, extraArguments, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                YoutubeLogger.Warning("عملیات توسط کاربر لغو شد.");
                throw;
            }
            catch (Exception ex)
            {
                YoutubeLogger.Error("خطای کلی در DownloadMovieTrailerAsync.", ex);

                return new YtDlpResult
                {
                    Success = false,
                    ExitCode = -1,
                    StandardError = ex.ToString()
                };
            }
            finally
            {
                YoutubeLogger.Info("در حال توقف Proxy...");
                _proxyManager.Stop();
                YoutubeLogger.Info("پایان عملیات دانلود Trailer.");
                YoutubeLogger.Info("========================================");
            }
        }

        private static string GetString(
            JsonElement root,
            string name)
        {
            return root.TryGetProperty(
                       name,
                       out JsonElement element)
                   ? element.GetString()
                     ?? string.Empty
                   : string.Empty;
        }

        private static int? GetInt(
            JsonElement root,
            string name)
        {
            if (!root.TryGetProperty(
                    name,
                    out JsonElement element))
            {
                return null;
            }

            return element.TryGetInt32(
                out int value)
                ? value
                : null;
        }

        private static long? GetLong(
            JsonElement root,
            string name)
        {
            if (!root.TryGetProperty(
                    name,
                    out JsonElement element))
            {
                return null;
            }

            return element.TryGetInt64(
                out long value)
                ? value
                : null;
        }
    }
}