namespace MyCom.Class;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// مدل یک نتیجه‌ی جستجو
public class TrailerCandidate
{
    public string Title { get; set; }
    public string Uploader { get; set; }
    public string Url { get; set; }
    public int? DurationSeconds { get; set; }
    public long? ViewCount { get; set; }
    public string UploadDate { get; set; }
}

// جستجوی تریلر در یوتیوب
public static class ClassYoutube
{
    public static async Task<List<TrailerCandidate>> SearchCandidates(string movieName, int year, int count = 15)
    {
        var searchQuery = $"ytsearch{count}:{movieName} {year} official trailer";
        var results = new List<TrailerCandidate>();

        Console.WriteLine($"[DEBUG] در حال جستجو: {searchQuery}");

        var psi = new ProcessStartInfo
        {
            FileName = "yt-dlp.exe",
            Arguments = $"--dump-json --no-download \"{searchQuery}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);

        var errorTask = process.StandardError.ReadToEndAsync();

        string line;
        int lineCount = 0;
        while ((line = await process.StandardOutput.ReadLineAsync()) != null)
        {
            lineCount++;
            if (string.IsNullOrWhiteSpace(line)) continue;

            try
            {
                using var doc = JsonDocument.Parse(line);
                var root = doc.RootElement;

                results.Add(new TrailerCandidate
                {
                    Title = root.GetProperty("title").GetString(),
                    Uploader = root.TryGetProperty("uploader", out var u) ? u.GetString() : "نامشخص",
                    Url = root.TryGetProperty("webpage_url", out var wu)
                        ? wu.GetString()
                        : $"https://www.youtube.com/watch?v={root.GetProperty("id").GetString()}",
                    DurationSeconds = root.TryGetProperty("duration", out var d) && d.ValueKind == JsonValueKind.Number ? d.GetInt32() : null,
                    ViewCount = root.TryGetProperty("view_count", out var v) && v.ValueKind == JsonValueKind.Number ? v.GetInt64() : null,
                    UploadDate = root.TryGetProperty("upload_date", out var ud) ? ud.GetString() : null
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] خطا در پارس خط JSON: {ex.Message}");
                Console.WriteLine($"[DEBUG] محتوای خط: {line}");
            }
        }

        await process.WaitForExitAsync();
        var errorOutput = await errorTask;

        Console.WriteLine($"[DEBUG] تعداد خطوط خروجی: {lineCount}");
        Console.WriteLine($"[DEBUG] تعداد نتایج پارس‌شده: {results.Count}");
        Console.WriteLine($"[DEBUG] Exit Code: {process.ExitCode}");

        if (!string.IsNullOrWhiteSpace(errorOutput))
            Console.WriteLine($"[DEBUG] stderr:\n{errorOutput}");

        return results;
    }
}

// انتخاب خودکار بهترین گزینه
public static class TrailerAutoSelector
{
    private static readonly string[] TrustedChannelKeywords =
    {
        "Pictures", "Studios", "Entertainment", "Films", "Movies", "Cinema", "Media", "Trailers"
    };

    private const int MaxDurationSeconds = 180; // سقف سه دقیقه

    public static TrailerCandidate SelectBest(List<TrailerCandidate> candidates, bool strictDuration = true)
    {
        if (candidates == null || candidates.Count == 0)
            return null;

        var pool = candidates.AsEnumerable();

        // فیلتر سخت: اول فقط اونایی که زیر سقف مدت زمان هستن رو در نظر بگیر
        if (strictDuration)
        {
            var filtered = candidates.Where(c => c.DurationSeconds.HasValue && c.DurationSeconds <= MaxDurationSeconds).ToList();
            if (filtered.Count > 0)
                pool = filtered;
            // اگه هیچی زیر سقف نبود، pool همون همه‌ی candidates باقی می‌مونه (fallback)
        }

        var scored = pool.Select(c =>
        {
            double score = 0;

            if (!string.IsNullOrEmpty(c.Uploader) &&
                TrustedChannelKeywords.Any(k => c.Uploader.Contains(k, StringComparison.OrdinalIgnoreCase)))
                score += 40;

            if (c.DurationSeconds.HasValue)
            {
                if (c.DurationSeconds >= 90 && c.DurationSeconds <= MaxDurationSeconds)
                    score += 30;
                else if (c.DurationSeconds > MaxDurationSeconds)
                    score -= (c.DurationSeconds.Value - MaxDurationSeconds); // هرچی طولانی‌تر، جریمه‌ی بیشتر
                else if (c.DurationSeconds < 60)
                    score -= 20;
            }

            if (!string.IsNullOrEmpty(c.Title))
            {
                var titleLower = c.Title.ToLowerInvariant();
                if (titleLower.Contains("official") && titleLower.Contains("trailer"))
                    score += 20;
                else if (titleLower.Contains("trailer"))
                    score += 10;

                if (titleLower.Contains("reaction") || titleLower.Contains("react") ||
                    titleLower.Contains("breakdown") || titleLower.Contains("review") ||
                    titleLower.Contains("explained") || titleLower.Contains("recap") ||
                    titleLower.Contains("full movie") || titleLower.Contains("compilation"))
                    score -= 50;
            }

            if (c.ViewCount.HasValue && c.ViewCount > 0)
                score += Math.Log10(c.ViewCount.Value) * 5;

            return (Candidate: c, Score: score);
        })
        .OrderByDescending(x => x.Score)
        .ToList();

        return scored.FirstOrDefault().Candidate;
    }
}

// دانلود از یوتیوب
public static class TrailerDownloader
{
    public enum TrailerQuality
    {
        Q480 = 480,
        Q720 = 720,
        Q1080 = 1080
    }
  
        public static async Task<bool> DownloadFromYoutube(
            string youtubeUrl,
            string outputFolder,
            string fileName,
            TrailerQuality quality = TrailerQuality.Q720)
        {
            Directory.CreateDirectory(outputFolder);

            var safeFileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
            var outputPath = Path.Combine(outputFolder, $"{safeFileName}.mp4");

            int height = (int)quality;
            var format = $"bestvideo[height<={height}][ext=mp4]+bestaudio[ext=m4a]/best[height<={height}][ext=mp4]/best[height<={height}]";

            var psi = new ProcessStartInfo
            {
                FileName = "yt-dlp.exe",
                Arguments = $"-f \"{format}\" -o \"{outputPath}\" \"{youtubeUrl}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            Console.WriteLine($"[DEBUG] کیفیت انتخابی: {height}p");

            using var process = Process.Start(psi);
            process.OutputDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
            process.ErrorDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await process.WaitForExitAsync();

            if (process.ExitCode == 0)
            {
                Console.WriteLine($"دانلود کامل شد: {outputPath}");
                return true;
            }

            Console.WriteLine("خطا در دانلود.");
            return false;
        }
    }


// نقطه‌ی ورود اصلی: جستجو + انتخاب خودکار + دانلود
public static class ClassYoutubeService
{
    public static async Task<bool> GetTrailer(
        string movieName,
        int year,
        string outputFolder,
        TrailerDownloader.TrailerQuality quality = TrailerDownloader.TrailerQuality.Q720)
    {
        var candidates = await ClassYoutube.SearchCandidates(movieName, year, count: 15);

        if (candidates.Count == 0)
        {
            Console.WriteLine("هیچ نتیجه‌ای برای این فیلم پیدا نشد.");
            return false;
        }

        var best = TrailerAutoSelector.SelectBest(candidates, strictDuration: true);

        if (best == null)
        {
            Console.WriteLine("گزینه‌ی مناسبی پیدا نشد.");
            return false;
        }

        var durationStr = best.DurationSeconds.HasValue
            ? TimeSpan.FromSeconds(best.DurationSeconds.Value).ToString(@"mm\:ss")
            : "؟";

        Console.WriteLine($"انتخاب شد: {best.Title} | کانال: {best.Uploader} | مدت: {durationStr}");

        var safeFileName = $"{movieName}_{year}";
        return await TrailerDownloader.DownloadFromYoutube(best.Url, outputFolder, safeFileName, quality);
    }
}