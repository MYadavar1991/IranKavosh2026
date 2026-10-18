using IranKavosh.Layered.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : Controller
    {

        // پوشه ذخیره صفحات موقت
        private readonly string _tempPagesFolder = @"C:\TempDownloadPages";

        public DownloadController()
        {
            // ایجاد پوشه اگر وجود ندارد
            if (!Directory.Exists(_tempPagesFolder))
                Directory.CreateDirectory(_tempPagesFolder);
        }

        [HttpPost("create-download-page")]
        public IActionResult CreateDownloadPage([FromBody] List<ModelFile> movies)
        {
            if (movies == null || movies.Count == 0)
                return BadRequest("هیچ فیلمی انتخاب نشده");

            // 1. ساختن GUID یکتا برای این مشتری
            string guid = Guid.NewGuid().ToString();

            // 2. ساخت صفحه HTML
            string html = BuildDownloadPage(movies, GetLocalIpAddress(), guid);

            // 3. ذخیره فایل HTML با نام GUID
            string htmlPath = Path.Combine(_tempPagesFolder, $"{guid}.html");
            System.IO.File.WriteAllText(htmlPath, html, Encoding.UTF8);

            // 4. ساخت لینک قابل دسترس
            string downloadUrl = $"http://{GetLocalIpAddress()}:1212/download-pages/{guid}.html";

            // 5. (اختیاری) ذخیره اطلاعات در دیتابیس یا کش برای مدیریت بعدی
            // می‌توانی زمان انقضا هم ذخیره کنی

            return Ok(new
            {
                link = downloadUrl,
                guid = guid,
                expiry = DateTime.Now.AddHours(24), // 24 ساعت معتبر
                movieCount = movies.Count
            });
        }

        [HttpGet("page-info/{guid}")]
        public IActionResult GetPageInfo(string guid)
        {
            string htmlPath = Path.Combine(_tempPagesFolder, $"{guid}.html");

            if (!System.IO.File.Exists(htmlPath))
                return NotFound("لینک منقضی شده یا وجود ندارد");

            return Ok(new
            {
                guid = guid,
                exists = true,
                created = System.IO.File.GetCreationTime(htmlPath)
            });
        }

        [HttpDelete("expire/{guid}")]
        public IActionResult ExpirePage(string guid)
        {
            string htmlPath = Path.Combine(_tempPagesFolder, $"{guid}.html");

            if (System.IO.File.Exists(htmlPath))
            {
                System.IO.File.Delete(htmlPath);
                return Ok("لینک منقضی شد");
            }

            return NotFound("لینک وجود ندارد");
        }

        private string BuildDownloadPage(List<ModelFile> movies, string serverIp, string guid)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='utf-8'/>");
            sb.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1'/>");
            sb.AppendLine($"<title>دانلود فیلم‌ها - {guid.Substring(0, 8)}</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Tahoma; background: #1a1a2e; color: white; padding: 20px; direction: rtl; }");
            sb.AppendLine("h1 { text-align: center; }");
            sb.AppendLine(".movie { background: #16213e; margin: 15px 0; padding: 20px; border-radius: 10px; }");
            sb.AppendLine("a { background: #e94560; color: white; padding: 10px 25px; text-decoration: none; border-radius: 5px; display: inline-block; }");
            sb.AppendLine(".info { text-align: center; color: #888; margin-top: 20px; font-size: 12px; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine($"<h1>🎬 {movies.Count} فیلم آماده دانلود</h1>");

            foreach (var movie in movies)
            {
                string encodedPath = Uri.EscapeDataString(movie.From);
                string encodedName = Uri.EscapeDataString(movie.Name);
                string downloadUrl = $"http://{serverIp}:1212/api/Download/download-single?path={encodedPath}&name={encodedName}";

                sb.AppendLine($"<div class='movie'>");
                sb.AppendLine($"<strong>📹 {movie.Name}</strong><br/><br/>");
                sb.AppendLine($"<a href='{downloadUrl}'>📥 دانلود / پخش</a>");
                sb.AppendLine($"</div>");
            }

            sb.AppendLine($"<div class='info'>");
            sb.AppendLine($"شناسه: {guid}<br/>");
            sb.AppendLine($"این لینک به مدت 24 ساعت معتبر است");
            sb.AppendLine($"</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        [HttpGet("download-single")]
        public IActionResult DownloadSingle([FromQuery] string path, [FromQuery] string name)
        {
            if (!System.IO.File.Exists(path))
                return NotFound();

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "video/mp4", $"{name}");
        }

        private string GetLocalIpAddress()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            var ip = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            return ip?.ToString() ?? "localhost";
        }
    }

    //[HttpPost("create-download-page")]
    //public IActionResult CreateDownloadPage([FromBody] List<ModelFile> movies)
    //{
    //    if (movies == null || movies.Count == 0)
    //        return BadRequest();

    //    var validMovies = movies.Where(m => System.IO.File.Exists(m.From)).ToList();

    //    // ساخت صفحه HTML با دانلود خودکار پشت سر هم
    //    var html = BuildAutoDownloadPage(validMovies);
    //    return Content(html, "text/html; charset=utf-8");
    //}

    //private string BuildAutoDownloadPage(List<ModelFile> movies)
    //{

    //    var sb = new StringBuilder();
    //    sb.AppendLine("<!DOCTYPE html>");
    //    sb.AppendLine("<html>");
    //    sb.AppendLine("<head>");
    //    sb.AppendLine("<meta charset='utf-8'/>");
    //    sb.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1'/>");
    //    sb.AppendLine("<title>دانلود فیلم‌ها</title>");
    //    sb.AppendLine("<style>");
    //    sb.AppendLine("body { font-family: Tahoma; background: #1a1a2e; color: white; padding: 20px; direction: rtl; }");
    //    sb.AppendLine("h1 { text-align: center; margin-bottom: 30px; }");
    //    sb.AppendLine(".container { max-width: 800px; margin: 0 auto; }");
    //    sb.AppendLine(".movie { background: #16213e; margin: 15px 0; padding: 20px; border-radius: 10px; display: flex; justify-content: space-between; align-items: center; flex-wrap: wrap; }");
    //    sb.AppendLine(".movie-name { font-size: 16px; font-weight: bold; }");
    //    sb.AppendLine(".download-btn { background: #e94560; color: white; padding: 10px 25px; text-decoration: none; border-radius: 5px; transition: 0.3s; display: inline-block; }");
    //    sb.AppendLine(".download-btn:hover { background: #c62a47; }");
    //    sb.AppendLine(".select-all { background: #0f3460; color: white; padding: 10px 20px; margin: 20px 0; border: none; border-radius: 5px; cursor: pointer; width: 100%; font-size: 16px; }");
    //    sb.AppendLine(".note { text-align: center; margin-top: 30px; color: #888; font-size: 12px; }");
    //    sb.AppendLine("@media (max-width: 600px) { .movie { flex-direction: column; gap: 10px; } }");
    //    sb.AppendLine("</style>");
    //    sb.AppendLine("</head>");
    //    sb.AppendLine("<body>");
    //    sb.AppendLine("<div class='container'>");
    //    sb.AppendLine($"<h1>🎬 {movies.Count} فیلم آماده دانلود</h1>");

    //    // دکمه انتخاب همه (برای کپی لینک)
    //    sb.AppendLine("<button class='select-all' onclick='selectAll()'>📋 کپی همه لینک‌ها</button>");

    //    foreach (var movie in movies)
    //    {
    //        string encodedPath = Uri.EscapeDataString(movie.From);
    //        string downloadUrl = $"http://192.168.1.160:1212/api/Download/download-single?path={encodedPath}&name={Uri.EscapeDataString(movie.Name)}";

    //        sb.AppendLine($"<div class='movie' id='movie-{Guid.NewGuid()}'>");
    //        sb.AppendLine($"<span class='movie-name'>📹 {movie.Name}</span>");
    //        sb.AppendLine($"<a class='download-btn' href='{downloadUrl}' download='{movie.Name}'>📥 دانلود / پخش</a>");
    //        sb.AppendLine($"</div>");
    //    }

    //    sb.AppendLine("<div class='note'>💡 برای دانلود روی هر فیلم کلیک کنید. فیلم در حافظه گوشی ذخیره می‌شود.</div>");
    //    sb.AppendLine("</div>");
    //    sb.AppendLine("<script>");
    //    sb.AppendLine("function selectAll() {");
    //    sb.AppendLine("    const links = [];");
    //    sb.AppendLine("    document.querySelectorAll('.download-btn').forEach(btn => {");
    //    sb.AppendLine("        links.push(btn.href);");
    //    sb.AppendLine("    });");
    //    sb.AppendLine("    navigator.clipboard.writeText(links.join('\\n'));");
    //    sb.AppendLine("    alert('همه لینک‌ها کپی شد! می‌توانی یکی یکی در مرورگر باز کنی.');");
    //    sb.AppendLine("}");
    //    sb.AppendLine("</script>");
    //    sb.AppendLine("</body>");
    //    sb.AppendLine("</html>");



    //    return sb.ToString();
    //}

    //[HttpGet("download-single")]
    //public IActionResult DownloadSingle([FromQuery] string path, [FromQuery] string name)
    //{
    //    if (!System.IO.File.Exists(path))
    //        return NotFound();

    //    var bytes = System.IO.File.ReadAllBytes(path);
    //    return File(bytes, "video/mp4", $"{name}");
    //}
}

