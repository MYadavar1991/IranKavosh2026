using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCom.Class
{
    public static class GlobalExceptionHandler
    {
        public static void Initialize(string appName = "برنامه من", bool showMessageBox = true, bool openFile = false)
        {
            Application.ThreadException += (s, e) =>
                HandleException(e.Exception, $"{appName} - UI Thread", showMessageBox, openFile);

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                HandleException(e.ExceptionObject as Exception, $"{appName} - AppDomain", showMessageBox, openFile);
            //catch (IOException ex) when (ex.InnerException is SocketException)

           
            //TaskScheduler.UnobservedTaskException += (s, e) =>
            //{
            //    #region MyRegion

            //    //// این خط باعث می‌شود برنامه در این نقطه متوقف شود (فقط در Debug)
            //    //if (System.Diagnostics.Debugger.IsAttached)
            //    //{
            //    //    System.Diagnostics.Debugger.Break();
            //    //}

            //    //// حالا می‌توانید e.Exception را بررسی کنید
            //    //var aggEx = e.Exception as AggregateException;
            //    //var flattened = aggEx?.Flatten();

            //    #endregion
            //    HandleException(e.Exception, $"{appName} - TaskScheduler", showMessageBox, openFile);
            //    e.SetObserved(); // جلوگیری از کرش
            //};
            //TaskCanceledException 
        }
        public static async Task SafeIoOperation(Func<Task> ioAction, string operationName)
        {
            try
            {
                await ioAction();
            }
            catch (IOException ioEx) when (ioEx.Message.Contains("aborted") ||
                                           ioEx.Message.Contains("I/O operation"))
            {
                // اینجا خطای دقیق I/O را داری
                string error = $@"
عملیات I/O قطع شد: {operationName}
StackTrace:
{ioEx.StackTrace}
";
                File.AppendAllText("IoErrors.txt", error + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // سایر خطاها
                HandleException(ex, operationName, true, false);
            }
        }
        private static void HandleException(Exception? ex, string source, bool showMessageBox, bool openFile )
        {
            if (ex == null) return;

            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogWin");
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);

                string fileName = $"error_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
                string fullPath = Path.Combine(logPath, fileName);

                var sb = new StringBuilder();
                sb.AppendLine($"Exception Source: {source}");
                sb.AppendLine($"Message: {ex.Message}");
                sb.AppendLine($"Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    sb.AppendLine($"Inner Exception: {ex.InnerException.Message}");
                    sb.AppendLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }

                File.WriteAllText(fullPath, sb.ToString());

                if (showMessageBox)
                    MessageBox.Show(ex.Message, @"خطای غیرمنتظره", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (openFile)
                    Process.Start(fullPath);
            }
            catch
            {
                // اگر لاگ‌گیری خودش خطا داد، سکوت کن
            }
        }
    }
}