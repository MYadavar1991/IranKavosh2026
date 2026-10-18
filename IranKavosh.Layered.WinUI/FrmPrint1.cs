using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using System.Printing;

namespace IranKavosh.Layered.WinUI
{
    public partial class FrmPrint1 : XtraForm
    {
        public FrmPrint1()
        {
            InitializeComponent();

        }


        public class PrintQueueMonitor
        {
            // تابع اصلی - هر بار صدا بزنی اطلاعات لحظه‌ای همه پرینترها رو میده
            public static Dictionary<string, int> GetAllPrintersJobCount()
            {
                var result = new Dictionary<string, int>();

                try
                {
                    using (LocalPrintServer printServer = new LocalPrintServer())
                    {
                        // گرفتن همه پرینترهای محلی و شبکه
                        PrintQueueCollection printers = printServer.GetPrintQueues(
                            new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });

                        foreach (PrintQueue printer in printers)
                        {
                            printer.Refresh();
                            result.Add(printer.Name, printer.NumberOfJobs);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"خطا: {ex.Message}");
                }

                return result;
            }

            // تابع با جزئیات بیشتر (نام پرینتر، تعداد کارها، وضعیت کاغذ)
            public static List<PrinterInfo> GetAllPrintersDetailedInfo()
            {
                var result = new List<PrinterInfo>();

                try
                {
                    using (LocalPrintServer printServer = new LocalPrintServer())
                    {
                        PrintQueueCollection printers = printServer.GetPrintQueues(
                            new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });

                        foreach (PrintQueue printer in printers)
                        {
                            printer.Refresh();

                            var info = new PrinterInfo
                            {
                                Name = printer.Name,
                                JobCount = printer.NumberOfJobs,
                                IsOutOfPaper = printer.IsOutOfPaper,
                                IsOffline = printer.IsOffline,
                                IsError = printer.IsInError
                            };

                            result.Add(info);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"خطا: {ex.Message}");
                }

                return result;
            }
        }

        public class PrinterInfo
        {
            public string Name { get; set; }
            public int JobCount { get; set; }
            public bool IsOutOfPaper { get; set; }
            public bool IsOffline { get; set; }
            public bool IsError { get; set; }

            public string Status
            {
                get
                {
                    if (IsOutOfPaper) return "تمام شدن کاغذ";
                    if (IsOffline) return "آفلاین";
                    if (IsError) return "خطا";
                    return "آماده";
                }
            }
        }
        static bool isRunning = true;
        private void FrmPrint1_Load(object sender, EventArgs e)
        {
            Thread monitorThread = new Thread(MonitorPrinters);
            monitorThread.IsBackground = true;
            monitorThread.Start();

            Console.WriteLine("در حال مانیتورینگ پرینترها... (برای خروج ESC بزنید)\n");

            //while (isRunning)
            //{
            //    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            //    {
            //        isRunning = false;
            //        break;
            //    }
            //    Thread.Sleep(100);
            //}
        }
        static void MonitorPrinters()
        {
            while (isRunning)
            {
                //Console.Clear();
                Console.WriteLine($"آخرین بروزرسانی: {DateTime.Now:HH:mm:ss}\n");

                // روش 1: فقط تعداد کارها
                var jobCounts = PrintQueueMonitor.GetAllPrintersJobCount();
                Console.WriteLine("=== وضعیت صف پرینترها ===\n");
                foreach (var printer in jobCounts)
                {
                    Console.WriteLine($"{printer.Key}: {printer.Value} کار در صف");
                }

                // روش 2: با جزئیات بیشتر
                Console.WriteLine("\n=== جزئیات کامل ===\n");
                var details = PrintQueueMonitor.GetAllPrintersDetailedInfo();
                foreach (var printer in details)
                {
                    if (printer.JobCount>0)
                    {
                        if (printer.Name.Contains("HP LaserJet Professional"))
                        {
                            Console.WriteLine($"پرینتر: {printer.Name}");
                            Console.WriteLine($"   تعداد کارها: {printer.JobCount}");
                            Console.WriteLine($"   وضعیت: {printer.Status}");
                            Console.WriteLine();
                        }
                    }
                }

                Thread.Sleep(3000); // هر 3 ثانیه یکبار بروزرسانی
            }
        }
    }
}