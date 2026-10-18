using IranKavosh.Layered.Service.Services;
using IranKavosh.Layered.WinUI.ClassReport;
using IranKavosh.Layered.WinUI.Report;
using MyCom.Class;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IranKavosh.Layered.WinUI.Class
{
    public class ClsPublic
    {
        static HttpClient _httpClient = new HttpClient();
        private PersonalHttpService _personal = new PersonalHttpService(_httpClient);
        private PersonalGroupHttpService _personalGroup = new PersonalGroupHttpService(_httpClient);

        private MultimediaHeaderHttpService _multiHeader = new MultimediaHeaderHttpService(_httpClient);
        private MultimediaHeaderChapterHttpService _multimediaHeaderChapter = new MultimediaHeaderChapterHttpService(_httpClient);
        private MultimediaChapterDetailHttpService _multiChapterDetail = new MultimediaChapterDetailHttpService(_httpClient);

        private MultiLocationHttpService _multiLocation = new MultiLocationHttpService(_httpClient);
        private MultiCountryHttpService _multiCountry = new MultiCountryHttpService(_httpClient);
        private MultiGenreHttpService _multiGenre = new MultiGenreHttpService(_httpClient);
        private HddMemoryHttpService _hddMemoryHttp = new HddMemoryHttpService(_httpClient);
        private HddMemoryPersonHttpService _hddMemoryPerson = new HddMemoryPersonHttpService(_httpClient);
        private DefinitiveAccountHttpService _definitiveAccount = new DefinitiveAccountHttpService(_httpClient);


        public async Task CreatePosterByHeader(Guid headerId)
        {
            //await Task.Delay(2000);
            var getHeader2 = (await _multiHeader.GetByHeaderTranslate(headerId));

            var modelChapterDetailsFull = getHeader2.MaxBy(m => m.ChapterCode);

            var getHeader = (await _multiHeader.GetById(modelChapterDetailsFull.HeaderId));
            var getMultimediaGroup = getHeader.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")
                ? "سریال"
                : "سینمایی";
            var getGenre = await _multiGenre.GetAsync();

            var getgenerList = getHeader.MultiGenreLists.Split(',');
            var genreTitles = getGenre
                .Where(g => getgenerList.Contains(g.Id.ToString()))
                .Select(g => g.Title);
            var genreTitlesJoin = string.Join(" ,", genreTitles);

            var getImage = modelChapterDetailsFull.ImageDir;
            var getDirImage = getImage.LoadBitmap().ImageToBase64();
            var poster = new ClassReport.ClassReport.ModelPoster
            {
                Name = modelChapterDetailsFull.HeaderName,
                Genere = genreTitlesJoin,
                Image = getDirImage,
                MultimediaHeader = null,
                Serial = false
            };

            if (getMultimediaGroup == "سریال")
            {
                poster.Serial = true;
                poster.MultimediaHeader = await _multiHeader.GetChapterCount(getHeader.Id);
            }
            var poster1 = new Poster1();
            poster1.Tag = poster;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var getDesk = Path.Combine(desktopPath, "پوستر فیلم");
            Directory.CreateDirectory(getDesk);

            string addressPoster = Path.Combine(getDesk, getHeader.NamePersian + ".jpg");

            await poster1.ExportToImageAsync(addressPoster);
            poster1.Dispose();
            //await Task.Delay(1000);
            // MessageBox.Show("1");
            byte[] imageBytes = await File.ReadAllBytesAsync(addressPoster);

            using (var ms = new MemoryStream(imageBytes))
            using (var originalImage = Image.FromStream(ms))
            {
                using (var resizedImage = ClassImage.ResizeByPercent(originalImage, 5))
                {
                    // حذف فایل اصلی
                    File.Delete(addressPoster);

                    // ذخیره تصویر جدید
                    resizedImage.Save(addressPoster, ImageFormat.Jpeg);
                }
            }
            //  MessageBox.Show("2");
            //ClassBale bale = new ClassBale("1723187126:DU8S4HAsbBK2II1RGcFFPbygzwUrh8BWMvQ", "@iktest");
            ClassBale bale = new ClassBale("1723187126:DU8S4HAsbBK2II1RGcFFPbygzwUrh8BWMvQ", "@caffenetik");


            string msgNew = @"🎬 سریال: بدنام (قسمت ۷)

📌 ژانر: جنایی / معمایی / اجتماعی

📅 زمان: دوشنبه‌ها در ایران کاوش



📢 آدرس کانال:
🆔 @[آيدي_گروه_شما]

🎁 ربات جایزه: @[آیدی_گروه_شما]

🏆 باعضویت در ربات و کانال ایران کاوش فیلم رایگان هدیه بگیرید !!!

🔥 منتظر نظرات شما هستیم. بدون اسپویل!";

            await bale.SendPhoto(new ClassBale.ModelMsg
            {
                AddressImage = addressPoster,
                Msg = msgNew,
               
            });
          
            //  MessageBox.Show("3");
            //MessageBox.Show(modelChapterDetailsFull.ChapterCode.ToString());
        }
        public async Task CreatePosterByChapterDetail(Guid chapterDetailId)
        {

            var getDetail = (await _multiChapterDetail.GetByHeaderTranslate([new()
                {
                    ChapterDetailId = chapterDetailId
                }]))
                .ToList();

            var modelChapterDetailsFull = getDetail.First();

            var getHeader = (await _multiHeader.GetById(modelChapterDetailsFull.HeaderId));
            var getMultimediaGroup = getHeader.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")
                ? "سریال"
                : "سینمایی";
            var getGenre = await _multiGenre.GetAsync();

            var getgenerList = getHeader.MultiGenreLists.Split(',');
            var genreTitles = getGenre
                .Where(g => getgenerList.Contains(g.Id.ToString()))
                .Select(g => g.Title);
            var genreTitlesJoin = string.Join(" ,", genreTitles);

            var getImage = modelChapterDetailsFull.ImageDir;
            var getDirImage = getImage.LoadBitmap().ImageToBase64();
            var poster = new ClassReport.ClassReport.ModelPoster
            {
                Name = modelChapterDetailsFull.HeaderName,
                Genere = genreTitlesJoin,
                Image = getDirImage,
                MultimediaHeader = null,
                Serial = false
            };

            if (getMultimediaGroup == "سریال")
            {
                poster.Serial = true;
                poster.MultimediaHeader = await _multiHeader.GetChapterCount(getHeader.Id);
            }
            var poster1 = new Poster1();
            poster1.Tag = poster;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var getDesk = Path.Combine(desktopPath, "پوستر فیلم");
            Directory.CreateDirectory(getDesk);

            string addressPoster = Path.Combine(getDesk, getHeader.NamePersian + ".jpg");

            await poster1.ExportToImageAsync(addressPoster);
            poster1.Dispose();
            //await Task.Delay(1000);
            byte[] imageBytes = File.ReadAllBytes(addressPoster);

            using (var ms = new MemoryStream(imageBytes))
            using (var originalImage = Image.FromStream(ms))
            {
                using (var resizedImage = ClassImage.ResizeByPercent(originalImage, 5))
                {
                    // حذف فایل اصلی
                    File.Delete(addressPoster);

                    // ذخیره تصویر جدید
                    resizedImage.Save(addressPoster, ImageFormat.Jpeg);
                }
            }

            //var getImageLow = ClassImage.ResizeByPercent(Image.FromFile(addressPoster),30);
            //getImageLow.Save(addressPoster);
            //123
            ClassBale bale = new ClassBale("1723187126:hNcmequsGAYsZ3ppEwzihz_h99crv_eT95c", "@caffenetik");
            //await bale.SendMessage(new ClassBale.ModelMsg
            //{
            //    Msg = "ارسال پیام توسط ربات"
            //});


            bale.SendPhoto(new ClassBale.ModelMsg
            {
                Msg = getHeader.NamePersian,
                AddressImage = addressPoster
            });
        }
    }
}
