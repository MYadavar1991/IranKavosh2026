using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;

namespace MyCom.Class.YouTube
{
    /// <summary>
    /// کلاس مدیریت پورت‌ها - بررسی و بستن فرآیندهای مرتبط با یک پورت
    /// </summary>
    public class PortManager
    {
        #region کلاس‌های کمکی

        /// <summary>
        /// اطلاعات یک فرآیند
        /// </summary>
        public class ProcessInfo
        {
            public int PID { get; set; }
            public string ProcessName { get; set; } = string.Empty;
            public string LocalAddress { get; set; } = string.Empty;
            public string RemoteAddress { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public bool IsXray { get; set; }

            public override string ToString()
            {
                return $"PID: {PID}, Name: {ProcessName}, State: {State}";
            }
        }

        /// <summary>
        /// نتیجه عملیات بستن پورت
        /// </summary>
        public class CloseResult
        {
            public int Port { get; set; }
            public List<ProcessInfo> KilledProcesses { get; set; } = new List<ProcessInfo>();
            public List<ProcessInfo> FailedProcesses { get; set; } = new List<ProcessInfo>();
            public bool IsPortFree { get; set; }
            public string Message { get; set; } = string.Empty;

            public override string ToString()
            {
                return $"پورت {Port}: {(IsPortFree ? "آزاد ✅" : "در حال استفاده ⚠️")} | بسته شد: {KilledProcesses.Count} | ناموفق: {FailedProcesses.Count}";
            }
        }

        #endregion

        #region متدهای اصلی

        /// <summary>
        /// دریافت لیست فرآیندهای مرتبط با یک پورت
        /// </summary>
        public static List<ProcessInfo> GetProcessesByPort(int port)
        {
            var result = new List<ProcessInfo>();
            var pids = new HashSet<int>();

            try
            {
                // روش اصلی: استفاده از netstat
                var output = RunNetstat();
                var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    if (!line.Contains($":{port}")) continue;

                    // جداسازی بخش‌ها
                    var parts = Regex.Split(line.Trim(), @"\s+");
                    if (parts.Length < 5) continue;

                    // استخراج PID (آخرین بخش)
                    var pidStr = parts[^1];
                    if (!int.TryParse(pidStr, out int pid)) continue;

                    if (!pids.Contains(pid))
                    {
                        pids.Add(pid);
                        try
                        {
                            var proc = Process.GetProcessById(pid);
                            if (!proc.HasExited)
                            {
                                result.Add(new ProcessInfo
                                {
                                    PID = pid,
                                    ProcessName = proc.ProcessName,
                                    LocalAddress = parts.Length > 1 ? parts[1] : "",
                                    RemoteAddress = parts.Length > 2 ? parts[2] : "",
                                    State = parts.Length > 3 ? parts[3] : "",
                                    IsXray = proc.ProcessName.ToLower().Contains("xray")
                                });
                            }
                        }
                        catch
                        {
                            // فرآیند وجود ندارد
                        }
                    }
                }

                // اگر نتیجه‌ای پیدا نشد، روش دوم: IPGlobalProperties
                if (result.Count == 0)
                {
                    result = GetProcessesFromIPGlobalProperties(port);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطا در دریافت فرآیندهای پورت {port}: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// بررسی اینکه آیا پورت در حال استفاده است
        /// </summary>
        public static bool IsPortInUse(int port)
        {
            return GetProcessesByPort(port).Count > 0;
        }

        /// <summary>
        /// بستن تمام فرآیندهای مرتبط با یک پورت
        /// </summary>
        public static CloseResult CloseProcessesOnPort(int port, bool showLogs = true, bool forceKill = true)
        {
            var result = new CloseResult { Port = port };

            if (showLogs)
                Console.WriteLine($"🔍 در حال جستجوی فرآیندهای مرتبط با پورت {port}...");

            var processes = GetProcessesByPort(port);

            if (processes.Count == 0)
            {
                if (showLogs)
                    Console.WriteLine($"ℹ️ هیچ فرآیندی روی پورت {port} پیدا نشد.");
                result.Message = "هیچ فرآیندی پیدا نشد";
                result.IsPortFree = true;
                return result;
            }

            if (showLogs)
            {
                Console.WriteLine($"✅ {processes.Count} فرآیند پیدا شد:");
                foreach (var p in processes)
                {
                    Console.WriteLine($"  • PID: {p.PID,-6} | نام: {p.ProcessName,-20} | وضعیت: {p.State}");
                }
                Console.WriteLine();
            }

            // بستن فرآیندها
            foreach (var pInfo in processes)
            {
                try
                {
                    var proc = Process.GetProcessById(pInfo.PID);
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                        if (forceKill)
                        {
                            // صبر برای بسته شدن کامل
                            proc.WaitForExit(3000);
                        }

                        if (proc.HasExited || forceKill)
                        {
                            result.KilledProcesses.Add(pInfo);
                            if (showLogs)
                                Console.WriteLine($"  ✅ {pInfo.ProcessName} (PID: {pInfo.PID}) بسته شد.");
                        }
                        else
                        {
                            result.FailedProcesses.Add(pInfo);
                            if (showLogs)
                                Console.WriteLine($"  ⚠️ {pInfo.ProcessName} (PID: {pInfo.PID}) بسته نشد.");
                        }
                    }
                    else
                    {
                        if (showLogs)
                            Console.WriteLine($"  ℹ️ {pInfo.ProcessName} (PID: {pInfo.PID}) قبلاً بسته شده بود.");
                    }
                }
                catch (Exception ex)
                {
                    result.FailedProcesses.Add(pInfo);
                    if (showLogs)
                        Console.WriteLine($"  ❌ خطا در بستن {pInfo.ProcessName} (PID: {pInfo.PID}): {ex.Message}");
                }
            }

            // بررسی نهایی
            Thread.Sleep(1000);
            var remaining = GetProcessesByPort(port);
            result.IsPortFree = remaining.Count == 0;

            if (showLogs)
            {
                Console.WriteLine();
                Console.WriteLine("📊 خلاصه:");
                Console.WriteLine($"  ✅ بسته شد: {result.KilledProcesses.Count}");
                Console.WriteLine($"  ❌ ناموفق: {result.FailedProcesses.Count}");
                Console.WriteLine($"  {(result.IsPortFree ? "✅" : "⚠️")} وضعیت پورت: {(result.IsPortFree ? "آزاد" : "در حال استفاده")}");
            }

            return result;
        }

        /// <summary>
        /// بستن سریع پورت بدون نمایش لاگ
        /// </summary>
        public static void ForceClosePort(int port)
        {
            CloseProcessesOnPort(port, false, true);
        }

        /// <summary>
        /// بستن پورت با نمایش لاگ
        /// </summary>
        public static void ClosePortWithLog(int port)
        {
            CloseProcessesOnPort(port, true, true);
        }

        #endregion

        #region متدهای خصوصی

        /// <summary>
        /// اجرای دستور netstat و برگرداندن خروجی
        /// </summary>
        private static string RunNetstat()
        {
            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "netstat",
                    Arguments = "-ano",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit(5000);
                return output;
            }
        }

        /// <summary>
        /// دریافت فرآیندها از طریق IPGlobalProperties (روش جایگزین)
        /// </summary>
        private static List<ProcessInfo> GetProcessesFromIPGlobalProperties(int port)
        {
            var result = new List<ProcessInfo>();

            try
            {
                var ipGlobalProps = IPGlobalProperties.GetIPGlobalProperties();
                var connections = ipGlobalProps.GetActiveTcpConnections();

                foreach (var conn in connections)
                {
                    if (conn.LocalEndPoint.Port == port || conn.RemoteEndPoint.Port == port)
                    {
                        // این روش PID ندارد، فقط برای نمایش اطلاعات استفاده می‌شود
                        result.Add(new ProcessInfo
                        {
                            PID = 0,
                            ProcessName = "Unknown",
                            LocalAddress = conn.LocalEndPoint.ToString(),
                            RemoteAddress = conn.RemoteEndPoint.ToString(),
                            State = conn.State.ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"خطا در IPGlobalProperties: {ex.Message}");
            }

            return result;
        }

        #endregion

        #region متدهای کمکی

        /// <summary>
        /// دریافت PIDهای مرتبط با یک پورت
        /// </summary>
        public static List<int> GetPIDsByPort(int port)
        {
            return GetProcessesByPort(port).Select(p => p.PID).ToList();
        }

        /// <summary>
        /// بررسی اینکه آیا پورت توسط Xray استفاده می‌شود
        /// </summary>
        public static bool IsXrayRunningOnPort(int port)
        {
            return GetProcessesByPort(port).Any(p => p.IsXray);
        }

        /// <summary>
        /// بستن فقط فرآیندهای Xray روی یک پورت
        /// </summary>
        public static void CloseXrayOnly(int port)
        {
            var processes = GetProcessesByPort(port).Where(p => p.IsXray).ToList();

            foreach (var p in processes)
            {
                try
                {
                    var proc = Process.GetProcessById(p.PID);
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                        Console.WriteLine($"✅ Xray (PID: {p.PID}) بسته شد.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ خطا: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// نمایش اطلاعات کامل پورت
        /// </summary>
        public static void ShowPortInfo(int port)
        {
            Console.WriteLine($"📋 اطلاعات پورت {port}:");
            Console.WriteLine($"  در حال استفاده: {(IsPortInUse(port) ? "بله" : "خیر")}");

            var processes = GetProcessesByPort(port);
            if (processes.Any())
            {
                Console.WriteLine($"  تعداد فرآیندها: {processes.Count}");
                foreach (var p in processes)
                {
                    Console.WriteLine($"    • {p}");
                }
            }
            else
            {
                Console.WriteLine("  هیچ فرآیندی یافت نشد.");
            }
        }

        #endregion
    }
}