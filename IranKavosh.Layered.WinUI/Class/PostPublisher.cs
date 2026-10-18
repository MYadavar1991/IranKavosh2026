// ---------------------------------------------------------------------------
//  PostPublisher.cs
//  ساخت «پست فیلم‌های جدید» و آپلود آن روی هاست
//
//  نیازمندی: NuGet  ->  FluentFTP  (نسخه 50 به بالا)
//  هر بار انتشار:
//    1) پوسترها کوچک می‌شوند (JPEG عرض 600، زیر 300KB)  -> 1.jpg ، 2.jpg ، ...
//    2) data.json ساخته می‌شود
//    3) پوشه‌ی شماره‌ی بعدی (آخرین شماره + 1) روی هاست آپلود می‌شود
//    4) لینک نهایی  https://سایت-تو/?p=شماره  برمی‌گردد
// ---------------------------------------------------------------------------

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentFTP;
using IranKavosh.Layered.Service.Middleware;

namespace IranKavosh.Layered.WinUI.Class; // ← اگر لازم بود با namespace پروژه‌ات عوض کن

/// <summary>تنظیمات هاست. مقدارها را از فایل تنظیمات برنامه بخوان؛ رمز را داخل کد نگذار.</summary>
public sealed class FtpSettings
{
    public string Host { get; set; } = "site95258.siteasp.net";
    public string User { get; set; } = "site95258";
    public string Password { get; set; } = "Bf8?=Fp6n3Y_";

    /// <summary>پوشه‌ای روی هاست که index.html داخل آن است (معمولاً /wwwroot)</summary>
    public string RemoteRoot { get; set; } = "/wwwroot";

    /// <summary>آدرس سایت بدون اسلش آخر، مثل https://irankavosh.runasp.net</summary>
    public string SiteBaseUrl { get; set; } = "https://irankavosh.runasp.net";
}

/// <summary>یک فیلم انتخاب‌شده + مسیر پوستر اصلی آن (همان Image.jpg روی شبکه)</summary>
public sealed record PostFilm(ModelMultiHeaderPoster Header, string PosterPath);

public sealed record PublishResult(int Number, string Url);

public sealed class PostPublisher
{
    private const int PosterWidth = 600;
    private const long PosterMaxBytes = 300 * 1024;
    private const int MaxGenres = 4;

    private readonly FtpSettings _s;
    public PostPublisher(FtpSettings settings) => _s = settings;

    // ---------------------------------------------------------------- انتشار
    /// <param name="films">به همان ترتیبی که می‌خواهی در اسلایدر بیایند (اولی در بنر اول نمایش داده می‌شود)</param>
    /// <param name="genreTitleById">شناسه‌ی ژانر (به‌صورت متن) ← عنوان ژانر</param>
    public async Task<PublishResult> PublishAsync(
        IReadOnlyList<PostFilm> films,
        IReadOnlyDictionary<string, string> genreTitleById,
        IProgress<string>? progress = null,
        CancellationToken ct = default)
    {
        if (films.Count == 0)
            throw new ArgumentException("هیچ فیلمی انتخاب نشده است.", nameof(films));

        var temp = Path.Combine(Path.GetTempPath(), "club-post-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(temp);

        try
        {
            // 1) پوسترها + آیتم‌های JSON
            var items = new List<PostItemDto>();
            for (int i = 0; i < films.Count; i++)
            {
                ct.ThrowIfCancellationRequested();
                var f = films[i];
                progress?.Report($"کوچک‌سازی پوستر {i + 1} از {films.Count}");

                if (!File.Exists(f.PosterPath))
                    throw new FileNotFoundException($"پوستر فیلم «{f.Header.NamePersian}» پیدا نشد.", f.PosterPath);

                var fileName = $"{i + 1}.jpg";
                var dest = Path.Combine(temp, fileName);
                var src = f.PosterPath;
                await Task.Run(() => SavePosterJpeg(src, dest), ct);

                items.Add(ToDto(f.Header, fileName, genreTitleById));
            }

            // 2) data.json
            var post = new PostDto { Title = BuildTitle(DateTime.Now), Items = items };
            var json = JsonSerializer.Serialize(post, JsonOpts);
            await File.WriteAllTextAsync(Path.Combine(temp, "data.json"), json, new System.Text.UTF8Encoding(false), ct);

            // 3) آپلود
            progress?.Report("اتصال به هاست...");
            using var ftp = CreateClient();
            await ftp.Connect(ct);

            var number = await NextNumberAsync(ftp, ct);
            var remoteDir = $"{_s.RemoteRoot.TrimEnd('/')}/{number}";

            progress?.Report($"آپلود پست شماره {number} ...");
            var results = await ftp.UploadDirectory(
                temp, remoteDir,
                FtpFolderSyncMode.Update,
                existsMode: FtpRemoteExists.Overwrite,
                token: ct);

            if (results.Any(r => r.IsFailed))
                throw new IOException("آپلود بعضی فایل‌ها ناموفق بود. دوباره تلاش کن.");

            progress?.Report("انجام شد");
            return new PublishResult(number, $"{_s.SiteBaseUrl.TrimEnd('/')}/?p={number}");
        }
        finally
        {
            try { Directory.Delete(temp, recursive: true); } catch { /* فایل موقت است */ }
        }
    }

    /// <summary>یک‌بار index.html را روی هاست بگذار (یا هر وقت تغییرش دادی)</summary>
    public async Task UploadIndexAsync(string localIndexHtml, CancellationToken ct = default)
    {
        using var ftp = CreateClient();
        await ftp.Connect(ct);
        await ftp.UploadFile(
            localIndexHtml,
            $"{_s.RemoteRoot.TrimEnd('/')}/index.html",
            existsMode: FtpRemoteExists.Overwrite,
            createRemoteDir: true,
            token: ct);
    }

    // ------------------------------------------------------------------- FTP
    private AsyncFtpClient CreateClient()
    {
        var ftp = new AsyncFtpClient(_s.Host, _s.User, _s.Password);
        // اگر هاست FTPS (TLS) پشتیبانی کند خودکار استفاده می‌شود و رمز بدون رمزنگاری نمی‌رود.
        ftp.Config.EncryptionMode = FtpEncryptionMode.Auto;
        // هاست‌های اشتراکی معمولاً گواهی‌ای دارند که با نام هاست نمی‌خواند.
        // فقط اگر خطای گواهی گرفتی این خط را فعال کن:
        // ftp.Config.ValidateAnyCertificate = true;
        return ftp;
    }

    /// <summary>بزرگ‌ترین پوشه‌ی عددی روی هاست + 1</summary>
    private async Task<int> NextNumberAsync(AsyncFtpClient ftp, CancellationToken ct)
    {
        var listing = await ftp.GetListing(_s.RemoteRoot, ct);
        int max = listing
            .Where(x => x.Type == FtpObjectType.Directory && int.TryParse(x.Name, out _))
            .Select(x => int.Parse(x.Name))
            .DefaultIfEmpty(0)
            .Max();
        return max + 1;
    }

    // ------------------------------------------------------------------ JSON
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,   // فارسی خوانا بماند
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private sealed class PostDto
    {
        public string Title { get; set; } = "";
        public List<PostItemDto> Items { get; set; } = new();
    }

    private sealed class PostItemDto
    {
        public string Name { get; set; } = "";
        public string? Year { get; set; }
        public string[] Genre { get; set; } = Array.Empty<string>();
        public string? Country { get; set; }
        public string? Type { get; set; }
        public bool IsAnimated { get; set; }
        public bool IsAnime { get; set; }
        public double? Rate { get; set; }
        public string? Cast { get; set; }
        public string? Sum { get; set; }
        public string Poster { get; set; } = "";
    }

    private static PostItemDto ToDto(ModelMultiHeaderPoster h, string posterFile, IReadOnlyDictionary<string, string> genreTitleById)
    {
        var genres = (h.MultiGenreLists ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(genreTitleById.ContainsKey)
            .Select(id => genreTitleById[id])
            .Take(MaxGenres)
            .ToArray();

        return new PostItemDto
        {
            Name = h.NamePersian,
            Year = NullIfEmpty(h.Created),
            Genre = genres,
            Country = NullIfEmpty(h.Country),          // «سایر» را خود صفحه نمایش نمی‌دهد
            Type = NullIfEmpty(h.MultimediaGroup),     // سریال | سینمایی
            IsAnimated = h.IsAnimated,
            IsAnime = h.IsAnime,
            Rate = h.IMDB > 0 ? Math.Round((double)h.IMDB, 1) : null,
            Cast = NullIfEmpty(h.Actors),
            Sum = NullIfEmpty(h.Description),
            Poster = posterFile
        };
    }

    private static string? NullIfEmpty(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    private static string BuildTitle(DateTime d)
    {
        var pc = new PersianCalendar();
        string[] months = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
                            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        return $"فیلم‌های جدید {pc.GetDayOfMonth(d)} {months[pc.GetMonth(d) - 1]}";
    }

    // ----------------------------------------------------------------- تصویر
    /// <summary>پوستر را به عرض 600 کوچک می‌کند و کیفیت JPEG را تا رسیدن به زیر 300KB کم می‌کند</summary>
    private static void SavePosterJpeg(string source, string dest)
    {
        using var img = Image.FromFile(source);
        int w = Math.Min(PosterWidth, img.Width);
        int h = (int)Math.Round(img.Height * (w / (double)img.Width));

        using var bmp = new Bitmap(w, h);
        using (var g = Graphics.FromImage(bmp))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(img, 0, 0, w, h);
        }

        var codec = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);

        for (long quality = 82; quality >= 40; quality -= 6)
        {
            using var ms = new MemoryStream();
            using var ps = new EncoderParameters(1);
            ps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            bmp.Save(ms, codec, ps);

            if (ms.Length <= PosterMaxBytes || quality <= 40)
            {
                File.WriteAllBytes(dest, ms.ToArray());
                return;
            }
        }
    }
}

/*
=============================== نمونه‌ی استفاده در فرم ===============================

private readonly PostPublisher _publisher = new(new FtpSettings
{
    Host        = "ftpهاست-تو",             // مثلاً از پنل MonsterASP
    User        = "...",
    Password    = "...",                      // از فایل تنظیمات / رمزشده بخوان
    RemoteRoot  = "/wwwroot",
    SiteBaseUrl = "https://سایت-تو.runasp.net"
});

private async void btnPublish_Click(object sender, EventArgs e)
{
    var selected = dgvVideo.SelectedRows.Cast<DataGridViewRow>()
        .Select(r => (ModelTileView)r.DataBoundItem)
        .OrderByDescending(t => t.Num)                 // جدیدترین اول
        .ToList();

    if (selected.Count == 0) { MessageBox.Show("حداقل یک فیلم انتخاب کن."); return; }

    btnPublish.Enabled = false;
    try
    {
        var locations = await _multiLocation.GetAsync();
        var genreMap  = getGenre.ToDictionary(g => g.Id.ToString(), g => g.Title);

        var films = new List<PostFilm>();
        foreach (var t in selected)
        {
            ModelMultiHeaderPoster h = await GetHeaderPosterAsync(t.Id);   // ← از سرویس خودت بگیر
            var dir    = locations.First(l => l.Id == t.LocationId).DirShare_PCIP;
            var poster = Path.Combine(dir, t.Num.ToString(), "Image.jpg");  // همان مسیر FunAddressAllImage
            films.Add(new PostFilm(h, poster));
        }

        var progress = new Progress<string>(msg => lblStatus.Text = msg);
        var result   = await _publisher.PublishAsync(films, genreMap, progress);

        Clipboard.SetText(result.Url);                  // روی رشته‌ی UI اجرا می‌شود
        MessageBox.Show($"پست {result.Number} منتشر شد و لینک کپی شد:\n{result.Url}");
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "خطا در انتشار", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally { btnPublish.Enabled = true; }
}
*/