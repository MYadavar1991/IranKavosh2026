using System;
using System.IO;
using System.Text;

namespace MyCom.Class.YouTube
{
    public static class YoutubeLogger
    {
        private static readonly object _lock = new();

        private static readonly string LogFilePath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "youtube.log");

        public static void Info(string message)
            => Write("INFO", message);

        public static void Debug(string message)
            => Write("DEBUG", message);

        public static void Warning(string message)
            => Write("WARNING", message);

        public static void Error(string message)
            => Write("ERROR", message);

        public static void Error(string message, Exception exception)
        {
            Write(
                "ERROR",
                $"{message}\n" +
                $"Exception: {exception.GetType().FullName}\n" +
                $"Message: {exception.Message}\n" +
                $"StackTrace:\n{exception.StackTrace}");
        }

        private static void Write(
            string level,
            string message)
        {
            try
            {
                string text =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] " +
                    $"[{level}] {message}";

                lock (_lock)
                {
                    File.AppendAllText(
                        LogFilePath,
                        text + Environment.NewLine,
                        Encoding.UTF8);
                }

                Console.WriteLine(text);
            }
            catch
            {
                // Logger نباید باعث Crash برنامه شود.
            }
        }
    }
}