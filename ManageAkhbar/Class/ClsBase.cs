using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static ManageAkhbar.Class.RssNewsService;

namespace ManageAkhbar.Class
{
    public class RssNewsService
    {
        private HttpClient _httpClient;

        public RssNewsService()
        {
            //_httpClient = new HttpClient();
            var handler = new SocketsHttpHandler
            {
                // 🔑 مجبور می‌کنه هر 2 دقیقه، یه اتصال جدید بگیره (DNS رو ریفرش می‌کنه)
                PooledConnectionLifetime = TimeSpan.FromMinutes(2),

                // بستن اتصال‌های بی‌استفاده برای جلوگیری از قفل شدن
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),

                // فعال کردن Compression برای کاهش مصرف پهنای باند
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler);
            // ⚠️ کلید اصلی موفقیت: User-Agent معتبر و جدید
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/150.0 (Windows NT 10.0; Win64; x64) Chrome/148.0.0.0 ");
        }
        public class NewsItem
        {
            public string Title { get; set; }          // عنوان خبر
            public string Description { get; set; }    // توضیحات / خلاصه خبر
            public string Link { get; set; }           // لینک به اصل خبر
            public DateTimeOffset PubDate { get; set; } // تاریخ انتشار
            public string Source { get; set; }         // نام خبرگزاری (مهر، تسنیم، ...)
            public string ImageUrl { get; set; }       // آدرس عکس خبر (در صورت وجود)
        }
        /// <summary>
        /// دریافت اخبار از یک فید RSS مشخص
        /// </summary>
        /// <param name="feedUrl">آدرس RSS (مثل https://www.mehrnews.com/rss)</param>
        /// <param name="sourceName">نام خبرگزاری (مثل "مهر")</param>
        public async Task<List<NewsItem>> FetchMehr(string feedUrl, string sourceName)
        {
            var newsList = new List<NewsItem>();

            try
            {
                // دریافت فایل XML از سرور
                var xml = await _httpClient.GetStringAsync(feedUrl);

                // خواندن XML و استخراج اخبار
                using (var stringReader = new StringReader(xml))
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    var feedReader = new RssFeedReader(xmlReader);

                    while (await feedReader.Read())
                    {
                        if (feedReader.ElementType == SyndicationElementType.Item)
                        {
                            var item = await feedReader.ReadItem();

                            // استخراج لینک خبر
                            string link = "";
                            var firstLink = item.Links?.FirstOrDefault();
                            if (firstLink != null)
                            {
                                link = firstLink.Uri?.ToString() ?? "";
                            }

                            newsList.Add(new NewsItem
                            {
                                Title = item.Title ?? "",
                                Description = item.Description ?? "",
                                Link = link,
                                PubDate = item.Published,
                                Source = sourceName
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // لاگ خطا برای این منبع خاص (اختیاری)
                Console.WriteLine($"خطا در دریافت از {sourceName}: {ex.Message}");
            }

            return newsList;
        }

        /// <summary>
        /// دریافت اخبار از چندین خبرگزاری به صورت همزمان
        /// </summary>
        /// <param name="feeds">دیکشنری حاوی (نام خبرگزاری، آدرس RSS)</param>
        //public async Task<List<NewsItem>> FetchAllFeedsAsync(Dictionary<string, string> feeds)
        //{
        //    var tasks = new List<Task<List<NewsItem>>>();

        //    // ساخت تسک‌های موازی برای هر خبرگزاری
        //    foreach (var feed in feeds)
        //    {
        //        tasks.Add(FetchFromRssAsync(feed.Value, feed.Key));
        //    }

        //    // اجرای همزمان همه و انتظار برای تکمیل
        //    var results = await Task.WhenAll(tasks);

        //    // جمع‌آوری نتایج
        //    var allNews = new List<NewsItem>();
        //    foreach (var result in results)
        //    {
        //        allNews.AddRange(result);
        //    }

        //    // مرتب‌سازی بر اساس تاریخ (جدیدترین اول)
        //    allNews.Sort((a, b) => b.PubDate.CompareTo(a.PubDate));

        //    return allNews;
        //}

        #region Convert RSS

        public async Task<List<NewsItem>> FetchTasnimToModelAsync(string feedUrl, string sourceName)
        {
            var newsList = new List<NewsItem>();

            try
            {
                var xml = await _httpClient.GetStringAsync(feedUrl);
                var doc = XDocument.Parse(xml);

                // فضای نام (Namespace) تگ‌های media را تعریف می‌کنیم
                XNamespace media = "http://search.yahoo.com/mrss/";

                var items = doc.Descendants("item");

                foreach (var item in items)
                {
                    // استخراج فیلدهای اصلی
                    var title = item.Element("title")?.Value ?? "";
                    var link = item.Element("link")?.Value ?? "";
                    var description = item.Element("description")?.Value ?? "";
                    var pubDateStr = item.Element("pubDate")?.Value ?? "";

                    // استخراج عکس (از تگ <media:content> یا <media:thumbnail>)
                    var mediaContent = item.Element(media + "content");
                    var mediaThumbnail = item.Element(media + "thumbnail");
                    string imageUrl = mediaContent?.Attribute("url")?.Value ??
                                      mediaThumbnail?.Attribute("url")?.Value ??
                                      "";

                    // تبدیل تاریخ
                    DateTimeOffset pubDate = DateTimeOffset.Now;
                    DateTimeOffset.TryParse(pubDateStr, out pubDate);

                    newsList.Add(new NewsItem
                    {
                        Title = title,
                        Link = link,
                        Description = description,
                        PubDate = pubDate,
                        Source = sourceName,
                        ImageUrl = imageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا در تبدیل تسنیم: {ex.Message}");
            }

            return newsList;
        }
        public async Task<List<NewsItem>> FetchIsna(string feedUrl, string sourceName)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true, // اجازه ریدایرکت بده
                UseCookies = true, // کوکی‌ها را نگه دار
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };

            _httpClient = new HttpClient(handler);

            // ==================== مهم‌ترین بخش ====================
            // کپی کردن تمام هدرهای یک مرورگر کروم واقعی
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,fa;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "none");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            _httpClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");



            var newsList = new List<NewsItem>();
            try
            {
                // شبیه‌سازی کامل یک درخواست مرورگر
                var request = new HttpRequestMessage(HttpMethod.Get, feedUrl);
                request.Headers.Add("Referer", "https://www.isna.ir/"); // مهم برای ایسنا

                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                // بررسی اینکه آیا پاسخ XML است یا HTML
                if (!content.TrimStart().StartsWith("<"))
                {
                    // ذخیره محتوای خطا برای دیباگ
                    var debugPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{sourceName}_error.html");
                    File.WriteAllText(debugPath, content);
                    throw new Exception($"سرور {sourceName} پاسخ HTML برگرداند. فایل خطا در {debugPath} ذخیره شد.");
                }

                var doc = XDocument.Parse(content);
                var items = doc.Descendants("item");

                foreach (var item in items)
                {
                    // استخراج فیلدها (مثل کد قبلی)
                    string title = item.Element("title")?.Value ?? "";
                    string link = item.Element("link")?.Value ?? "";
                    string description = item.Element("description")?.Value ?? "";
                    string imageUrl = "";

                    var enclosure = item.Element("enclosure");
                    if (enclosure != null)
                        imageUrl = enclosure.Attribute("url")?.Value ?? "";

                    newsList.Add(new NewsItem
                    {
                        Title = title,
                        Link = link,
                        Description = description,
                        PubDate = DateTimeOffset.Now, // یا از PubDate استفاده کن
                        Source = sourceName,
                        ImageUrl = imageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا در {sourceName}: {ex.Message}");
            }
            return newsList;
        }






        public async Task<List<NewsItem>> FetchFararu(string feedUrl, string sourceName)
        {
            var newsList = new List<NewsItem>();
            try
            {
                var xml = await _httpClient.GetStringAsync(feedUrl);
                var doc = XDocument.Parse(xml);
                var items = doc.Descendants("item");

                foreach (var item in items)
                {
                    // استخراج عنوان و لینک
                    string title = item.Element("title")?.Value ?? "";
                    string link = item.Element("link")?.Value ?? "";

                    // استخراج و تمیز کردن توضیحات از تگ‌های HTML
                    string rawDescription = item.Element("description")?.Value ?? "";
                    string cleanDescription = RemoveHtmlTags(rawDescription);

                    // استخراج تاریخ انتشار
                    DateTimeOffset pubDate = DateTimeOffset.Now;
                    string pubDateStr = item.Element("pubDate")?.Value ?? "";
                    if (!string.IsNullOrEmpty(pubDateStr))
                        DateTimeOffset.TryParse(pubDateStr, out pubDate);

                    // استخراج عکس از درون تگ <description>
                    string imageUrl = ExtractImageFromHtml(rawDescription);

                    newsList.Add(new NewsItem
                    {
                        Title = title,
                        Link = link,
                        Description = cleanDescription,
                        PubDate = pubDate,
                        Source = sourceName,
                        ImageUrl = imageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"خطا در دریافت/تبدیل {sourceName}: {ex.Message}");
            }
            return newsList;
        }

        // تابع کمکی برای حذف تگ‌های HTML از متن توضیحات
        private string RemoveHtmlTags(string html)
        {
            if (string.IsNullOrEmpty(html)) return "";
            return System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", " ").Trim();
        }

        // تابع کمکی برای استخراج آدرس عکس از درون تگ <img>
        private string ExtractImageFromHtml(string html)
        {
            if (string.IsNullOrEmpty(html)) return "";
            var match = System.Text.RegularExpressions.Regex.Match(html, "<img.+?src=[\"'](.+?)[\"'].+?>");
            return match.Success ? match.Groups[1].Value : "";
        }



        #endregion
    }
    public class NewsSources
    {
        public static Dictionary<string, string> GetMehr()
        {
            return new Dictionary<string, string>
            {
                { "مهر", "https://www.mehrnews.com/rss" },
            };
        }
        public static Dictionary<string, string> GetTasnim()
        {
            return new Dictionary<string, string>
            {
                { "تسنیم", "https://www.tasnimnews.ir/fa/rss/feeds/7/0/0/0" },
                { "1 تسنیم", "https://www.tasnimnews.ir/fa/rss/feeds/1407/0/0/0" },
            };
        }
        //public static Dictionary<string, string> GetIsna()
        //{
        //    return new Dictionary<string, string>
        //    {

        //        { "ایسنا قم", "https://www.isna.ir/rss/tp/276" },
            
        //    };
        //}
        public static Dictionary<string, string> GetFeraro()
        {
            return new Dictionary<string, string>
            {

            
                { "فرارو", "https://fararu.com/feeds" },
  
            };
        }
        public static Dictionary<string, string> GetIsna1()
        {
            return new Dictionary<string, string>
            {


                //{ "ایرنا", "https://www.irna.ir/rss" },
                //{ "فارس", "https://www.farsnews.ir/rss" },
                //{ "ایلنا", "https://www.ilna.ir/rss" },
                //{ "عصر ایران", "https://www.asriran.com/fa/rss" },
                //{ "یجی‌سی (اقتصادی)", "https://yjcnews.ir/fa/rss/6" },
                //{ "یجی‌سی (اجتماعی)", "https://yjcnews.ir/fa/rss/5" },
                //{ "یجی‌سی (خودرو)", "https://yjcnews.ir/fa/rss/6/152" }
            };
        }
        // اگر فقط چند خبرگزاری خاص را می‌خواهید
        public static Dictionary<string, string> GetMainFeeds()
        {
            return new Dictionary<string, string>
            {
                { "مهر", "https://www.mehrnews.com/rss" },
                { "تسنیم", "https://www.tasnimnews.com/fa/rss/" },
                { "ایسنا", "https://www.isna.ir/rss" },
                { "یجی‌سی (خودرو)", "https://yjcnews.ir/fa/rss/6/152" }  // مخصوص اخبار خودرو
            };
        }

    }
}
