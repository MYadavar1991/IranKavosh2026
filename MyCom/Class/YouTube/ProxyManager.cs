using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace MyCom.Class.YouTube
{
    public static class SsLinkToXrayJson
    {
        public sealed record SsInfo(string Method, string Password, string Host, int Port, string Tag);

        /// <summary>
        /// یک لینک ss:// رو پارس کرده و JSON کانفیگ (socks-in + shadowsocks-out) رو برمی‌گردونه.
        /// </summary>
        /// <param name="link">لینک ss:// کامل</param>
        /// <param name="socksPort">پورت inbound سوکس محلی (پیش‌فرض 10808)</param>
        /// <param name="indented">اگر true باشه، خروجی JSON فرمت‌شده (چندخطی) برمی‌گرده</param>
        public static string ToJsonConfig(string link, int socksPort = 10808, bool indented = true)
        {
            var info = Parse(link);
            return BuildConfig(info, socksPort, indented);
        }

        /// <summary>
        /// همون خروجی ToJsonConfig ولی به شکل JsonObject (برای دستکاری بیشتر قبل از سریالایز کردن).
        /// </summary>
        public static JsonObject ToJsonObject(string link, int socksPort = 10808)
        {
            var info = Parse(link);
            return BuildConfigObject(info, socksPort);
        }

        /// <summary>
        /// فقط پارس کردن لینک و برگردوندن اطلاعات (method, password, host, port, tag) بدون ساخت JSON.
        /// </summary>
        public static SsInfo Parse(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                throw new ArgumentException("لینک خالی است.", nameof(link));

            if (!link.StartsWith("ss://", StringComparison.OrdinalIgnoreCase))
                throw new FormatException("لینک باید با ss:// شروع بشه.");

            string rest = link.Substring("ss://".Length);

            string tag = "";
            int hashIdx = rest.IndexOf('#');
            if (hashIdx >= 0)
            {
                tag = Uri.UnescapeDataString(rest.Substring(hashIdx + 1));
                rest = rest.Substring(0, hashIdx);
            }

            int qIdx = rest.IndexOf('?');
            if (qIdx >= 0)
                rest = rest.Substring(0, qIdx);

            int atIdx = rest.LastIndexOf('@');
            if (atIdx < 0)
                throw new FormatException("علامت @ در لینک پیدا نشد (فرمت SIP002 نیست).");

            string userInfoB64 = rest.Substring(0, atIdx);
            string hostPort = rest.Substring(atIdx + 1);

            string userInfo = DecodeBase64Flexible(userInfoB64);

            int colonIdx = userInfo.IndexOf(':');
            if (colonIdx < 0)
                throw new FormatException("بخش method:password معتبر نیست.");

            string method = userInfo.Substring(0, colonIdx);
            string password = userInfo.Substring(colonIdx + 1);

            int portColonIdx = hostPort.LastIndexOf(':');
            if (portColonIdx < 0)
                throw new FormatException("پورت در لینک پیدا نشد.");

            string host = hostPort.Substring(0, portColonIdx);
            string portStr = hostPort.Substring(portColonIdx + 1);

            if (!int.TryParse(portStr, out int port))
                throw new FormatException($"پورت نامعتبر است: {portStr}");

            return new SsInfo(method, password, host, port, tag);
        }

        private static string DecodeBase64Flexible(string input)
        {
            string normalized = input.Replace('-', '+').Replace('_', '/');
            int mod = normalized.Length % 4;
            if (mod != 0)
                normalized = normalized.PadRight(normalized.Length + (4 - mod), '=');

            byte[] bytes = Convert.FromBase64String(normalized);
            return Encoding.UTF8.GetString(bytes);
        }

        private static JsonObject BuildConfigObject(SsInfo info, int socksPort)
        {
            return new JsonObject
            {
                ["log"] = new JsonObject
                {
                    ["loglevel"] = "warning"
                },
                ["inbounds"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["tag"] = "socks-in",
                        ["listen"] = "127.0.0.1",
                        ["port"] = socksPort,
                        ["protocol"] = "socks",
                        ["settings"] = new JsonObject
                        {
                            ["udp"] = true
                        }
                    }
                },
                ["outbounds"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["tag"] = "proxy",
                        ["protocol"] = "shadowsocks",
                        ["settings"] = new JsonObject
                        {
                            ["servers"] = new JsonArray
                            {
                                new JsonObject
                                {
                                    ["address"] = info.Host,
                                    ["port"] = info.Port,
                                    ["method"] = info.Method,
                                    ["password"] = info.Password,
                                    ["uot"] = false
                                }
                            }
                        }
                    }
                }
            };
        }

        private static string BuildConfig(SsInfo info, int socksPort, bool indented)
        {
            var root = BuildConfigObject(info, socksPort);
            var options = new JsonSerializerOptions { WriteIndented = indented };
            return root.ToJsonString(options);
        }
    }

    public sealed class ProxyManager : IDisposable
    {
        private Process? _xrayProcess;

        public int SocksPort { get; }

        public bool IsRunning => _xrayProcess != null && !_xrayProcess.HasExited;

        public string ProxyUrl => $"socks5h://127.0.0.1:{SocksPort}";

        public ProxyManager(int socksPort = 10808)
        {
            SocksPort = socksPort;

            YoutubeLogger.Debug($"ProxyManager ساخته شد. Port: {SocksPort}");
        }

        public async Task<bool> StartAsync(int timeoutMs = 15000, CancellationToken cancellationToken = default)
        {
            if (IsRunning)
            {
                YoutubeLogger.Debug("Xray از قبل توسط همین ProxyManager در حال اجراست.");

                return await WaitUntilReadyAsync(timeoutMs, cancellationToken);
            }

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string xrayDirectory = Path.Combine(baseDirectory, "Xray-windows-64");
            string xrayPath = Path.Combine(xrayDirectory, "xray.exe");
            string configPath = Path.Combine(xrayDirectory, "config.json");

            if (!File.Exists(configPath))
            {
                var getxrayConfig = SsLinkToXrayJson.ToJsonConfig("ss://YWVzLTI1Ni1nY206YjR0b3htY3VsZnNiYmg2cA@fnl-4.linkman.pro:8174?#yadavar-20.00GB%F0%9F%93%8A-30D%E2%8F%B3");
                await File.WriteAllTextAsync(configPath, getxrayConfig, cancellationToken);
                await Task.Delay(1000, cancellationToken);
            }

            YoutubeLogger.Info("شروع راه‌اندازی Proxy.");

            YoutubeLogger.Debug($"Xray Path: {xrayPath}");

            YoutubeLogger.Debug($"Config Path: {configPath}");

            if (!File.Exists(xrayPath))
            {
                YoutubeLogger.Error($"xray.exe پیدا نشد: {xrayPath}");

                throw new FileNotFoundException("فایل xray.exe پیدا نشد.", xrayPath);
            }

            if (!File.Exists(configPath))
            {
                YoutubeLogger.Error($"config.json پیدا نشد: {configPath}");

                throw new FileNotFoundException("فایل config.json پیدا نشد.", configPath);
            }

            // اگر پورت از قبل اشغال باشد،
            // Xray جدید را اجرا نمی‌کنیم.
            if (await IsPortOpenAsync("127.0.0.1", SocksPort, 500))
            {
                YoutubeLogger.Error($"پورت {SocksPort} از قبل اشغال است. " + "Xray جدید اجرا نشد.");

                return false;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = xrayPath,
                Arguments = $"run -c \"{configPath}\"",
                WorkingDirectory = xrayDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            _xrayProcess = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            _xrayProcess.OutputDataReceived += (_, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                    {
                        YoutubeLogger.Debug($"[XRAY STDOUT] {e.Data}");
                    }
                };

            _xrayProcess.ErrorDataReceived +=
                (_, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                    {
                        YoutubeLogger.Error(
                            $"[XRAY STDERR] {e.Data}");
                    }
                };

            _xrayProcess.Start();

            YoutubeLogger.Info($"Xray با موفقیت Start شد. PID: {_xrayProcess.Id}");

            _xrayProcess.BeginOutputReadLine();
            _xrayProcess.BeginErrorReadLine();

            bool ready =
                await WaitUntilReadyAsync(
                    timeoutMs,
                    cancellationToken);

            if (!ready)
            {
                YoutubeLogger.Error("Xray قبل از آماده‌شدن متوقف شد یا Proxy آماده نشد.");

                Stop();

                return false;
            }

            YoutubeLogger.Info(
                $"Proxy آماده شد: {ProxyUrl}");

            return true;
        }

        public async Task<bool> WaitUntilReadyAsync(
            int timeoutMs = 15000,
            CancellationToken cancellationToken = default)
        {
            Stopwatch stopwatch =
                Stopwatch.StartNew();

            while (stopwatch.ElapsedMilliseconds < timeoutMs)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (_xrayProcess != null &&
                    _xrayProcess.HasExited)
                {
                    YoutubeLogger.Warning(
                        "فرآیند Xray قبل از آماده‌شدن متوقف شد.");

                    return false;
                }

                if (await IsPortOpenAsync(
                        "127.0.0.1",
                        SocksPort,
                        1000))
                {
                    return true;
                }

                await Task.Delay(
                    250,
                    cancellationToken);
            }

            return false;
        }

        private static async Task<bool> IsPortOpenAsync(
            string host,
            int port,
            int timeoutMs)
        {
            try
            {
                using var client =
                    new TcpClient();

                Task connectTask =
                    client.ConnectAsync(
                        host,
                        port);

                Task timeoutTask =
                    Task.Delay(timeoutMs);

                Task completed =
                    await Task.WhenAny(
                        connectTask,
                        timeoutTask);

                return completed == connectTask &&
                       client.Connected;
            }
            catch
            {
                return false;
            }
        }

        public void Stop()
        {
            if (_xrayProcess == null)
            {
                YoutubeLogger.Debug(
                    "درخواست Stop دریافت شد اما Xray وجود نداشت.");

                return;
            }

            try
            {
                if (!_xrayProcess.HasExited)
                {
                    YoutubeLogger.Info(
                        $"در حال توقف Xray. PID: {_xrayProcess.Id}");

                    _xrayProcess.Kill(
                        entireProcessTree: true);

                    _xrayProcess.WaitForExit(5000);

                    YoutubeLogger.Info(
                        "Xray با موفقیت متوقف شد.");
                }
            }
            catch (Exception ex)
            {
                YoutubeLogger.Error(
                    "خطا هنگام توقف Xray.",
                    ex);
            }
            finally
            {
                _xrayProcess.Dispose();
                _xrayProcess = null;
            }
        }

        public void Dispose()
        {
            Stop();
        }

    }
}