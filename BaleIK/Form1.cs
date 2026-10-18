using BaleBotNet;
using BaleBotNet.Methods;
using BaleBotNet.Types; // فرض بر استفاده از کتابخانه BaleBotNet
using BaleIK.Class;
using DevExpress.Export;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using MyCom.Class;
using System.Net.Sockets;
using Message = BaleBotNet.Types.Message;

namespace BaleIK
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient _httpClient = new();
        private readonly MultiAgeHttpService _multiAge = new(_httpClient);
        private readonly MultiActorHttpService _multiActor = new(_httpClient);
        private readonly MultiGenreHttpService _multiGenre = new(_httpClient);
        private readonly MultiCountryHttpService _multiCountry = new(_httpClient);
        private readonly MultiPriceHttpService _multiPrice = new(_httpClient);
        private readonly MultimediaHeaderHttpService _multiHeader = new(_httpClient);
        private readonly MultimediaHeaderChapterHttpService _multiHeaderChapter = new(_httpClient);
        private readonly ChanelMemberHttpService _chanelMember = new(_httpClient);
        private readonly PersonalHttpService _personal = new(_httpClient);
        private ClsBot bot;
        private System.Windows.Forms.Timer pollTimer;

        public Form1()
        {
            InitializeComponent();
            InitializeBot();
            //Refral(123, 312.ToString());
        }
        private void InitializeBot()
        {
            bot = new ClsBot();

            // اتصال رویدادها
            bot.OnMessageReceived += Bot_OnMessageReceived;
            bot.OnError += Bot_OnError;
            //bot.OnCallbackReceived += Bot_OnCallbackReceived; ;  // ✅ اضافه شد
            // شروع تایمر برای بررسی پیام‌ها (هر 2 ثانیه)
            bot.OnTextCommand += Bot_OnTextCommand; ;  // ✅ اضافه شد

            pollTimer = new System.Windows.Forms.Timer();
            pollTimer.Interval = 2000;
            pollTimer.Tick += async (sender, e) => await PollMessages();
            pollTimer.Start();

            // lblStatus.Text = "✅ ربات فعال است...";
        }

        private async void Bot_OnTextCommand(long userId, string text)
        {

            if (InvokeRequired)
            {
                await InvokeAsync(() => Bot_OnTextCommand(userId, text));
                return;
            }


            //if (text.Contains("/start ref"))
            //{
            //    var getRef = text.Replace("/start ref_", "");
            //    //await Refral(userId, getRef);
            //}
            //else 
            if (text == ("ایرانی") || text == ("خارجی"))
            {
                await bot.Menu_Video_After(userId, text);
            }
            else
                switch (text)
                {

                    case "🔑 کلمه عبور و نام کاربری":
                        await GetUserPass(userId);
                        break;

                    case "مینی اپ":
                        {
                            //https://moharam1370.github.io/IkService/
                            
                        }
                        break;
                    case "طراحی":
                        await bot.Menu_Desine(userId);
                        break;

                    #region فیلم ها

                    case "فیلم ها":
                        await bot.Menu_Video(userId);
                        break;

                    case $"سریال های در حال پخش ({"ایرانی"})":
                        await LstSerialIraniNew(userId);
                        break;

                    case $"سینمایی جدید ({"ایرانی"})":
                        await LstSinemaIraniNew(userId);
                        break;

                    case $"سریال تمام شده ({"ایرانی"})":
                        await LstSerialIraniFinish(userId);
                        break;

                    case $"سینمایی قدیمی ({"ایرانی"})":
                        await LstSinemaIraniOld(userId);
                        break;

                    case $"سریال های در حال پخش ({"خارجی"})":
                        //await LstSerialIKhareji(userId);
                        break;

                    case $"سینمایی جدید ({"خارجی"})":
                        await LstSinemaKharejiNew(userId);
                        break;

                    case $"سینمایی قدیمی ({"خارجی"})":
                        await LstSinemaKharejiOld(userId);
                        break;
                    case "هندی":
                        await LstHindi(userId);
                        break;
                    case "ترکی":
                        await LstTorki(userId);
                        break;
                    case "انیمیشن":
                        await LstSinemaAnimation(userId);
                        break;
                    case "انیمه":
                        await LstSinemaAnime(userId);
                        break;
                    #endregion

                    case "بازگشت":
                        await bot.Menu_Main(userId);
                        break;

                    case "بازگشت به فیلم":
                        await bot.Menu_Video(userId);
                        break;

                    case "🎁 لینک دعوت و جوایز":
                        await Davat(userId);
                        break;

                }
            //case "":
            //    await Ghan(userId);
            //    break;
            //default:
            //    // پاسخ پیش‌فرض برای پیام‌های ناشناخته
            //    await bot.SendMessage(userId, "❌ متوجه نشدم. لطفاً از گزینه‌های منو استفاده کنید.");
            //    break;
        }

        #region Event


        public async Task GetUserPass(long userId)
        {
            var getPerson = (await _personal.GetAsync()).First(f => f.BotUserId == userId);
            if (getPerson.PersonalGroupId == Guid.Parse("b83cce9c-fdba-4f06-a4cb-dfabfb60aaed") || getPerson.PersonalGroupId == Guid.Parse("a3847121-4d17-4ce3-8fc2-fe45ec206344"))
            {
                Random rnd = new Random();
                int randomNumber = rnd.Next(100000, 1000000);
                await _personal.UpdatePassword(userId, randomNumber.ToString());

                string data = $"Username: {getPerson.Username}" + Environment.NewLine + $"Password: {randomNumber}";
                await bot.SendMessage(userId, data);
            }
            else
            {
                await bot.SendMessage(userId, "⚠️ !هویت تایید نشد ⚠️");
            }
        }

        public async Task Refral(long userId, string referralCode)
        {
            await _chanelMember.AddReferral(new ModelBot.ModelReferral
            {
                ReferralSend = userId,
                ReferralReceive = referralCode.GetNum<long>(),

            });
        }

        public async Task Davat(long userId)
        {
            #region بدست آوردن تعداد دعوت ها

            var getChanelMember = await _chanelMember.GetAsync();
            var getCountMember = getChanelMember.Count(w => w.ReferralSend == userId);

            #endregion

            //http://ble.ir/@IK2026bot
            //http://ble.ir/@caffenetik
            string referralLink = $"https://ble.ir/{"@IK2026bot"}?start=ref_{userId}";

            // دریافت تعداد دعوت‌های موفق این کاربر
            //int referralCount = await GetUserReferralCount(userId);
            //int remainingForReward = 3 - (referralCount % 3);
            string message = $@"
🎁 *سیستم دعوت از دوستان*

لینک دعوت اختصاصی شما:
`{referralLink}`

📊 *دعوت شده ها:* {getCountMember} نفر
* لیست جوایز
🎬 *1 تا 3 عضو جدید، به ازای هر عضو: 1 فیلم هدیه*
🎬 *4 تا 6 عضو جدید، به ازای هر عضو: 2 فیلم هدیه*
🎬 *6 تا 10 عضو جدید، به ازای هر عضو: 3 فیلم هدیه*



📌 *برای دریافت جایزه:*
پس از کسب امتیاز کافی، به ایران کاوش مراجعه کنید:

🔑 *کد تخفیف شما:* `{userId}`";

            var getBotP = await bot.SendMessage(userId, message);

            //// همچنین می‌توانید یک دکمه اینلاین برای اشتراک‌گذاری راحت‌تر اضافه کنید
            //var shareKeyboard = new InlineKeyboardMarkup
            //{
            //    InlineKeyboard =
            //    [
            //        [
            //            new InlineKeyboardButton
            //            {
            //                Text = "📤 اشتراک‌گذاری لینک",
            //                Url = referralLink,  // با کلیک، کاربر می‌تواند لینک را برای دیگران بفرستد
            //                CallbackData = "ShareLink"
            //            }
            //        ]
            //    ]
            //};


            //await getBotP.SendMessage(
            //    chatId: userId.ToString(),
            //    text: "لینک دعوت از دوستان:",
            //    replyMarkup: shareKeyboard
            //);
        }

        public async Task Tablighat(long userId)
        {


            //http://ble.ir/@IK2026bot
            //http://ble.ir/@caffenetik
            string referralLink = $"https://ble.ir/{"@IK2026bot"}?start=ref_{userId}";


            string message = $@"
🎁 *سیستم دعوت از دوستان*


🎬 *1 تا 3 عضو جدید، به ازای هر عضو: 1 فیلم هدیه*
";

            await bot.SendMessage(userId, message);


        }
        public async Task LstSinemaIraniNew(long userId)
        {
            var getDt = DateTime.Now.AddDays(-7);
            var getHeaderChapter = (await _multiHeaderChapter.GetAsync());

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.DateTime >= getDt &&
                            w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") &&
                            w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5"))
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek,
                    CapterCount = getHeaderChapter.Count(c => c.MultimediaHeaderId == s.Id)
                })
                .OrderByDescending(o => o.Num).Take(5);

            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        public async Task LstSinemaKharejiNew(long userId)
        {
            var getDt = DateTime.Now.AddDays(-15);
            var getHeaderChapter = (await _multiHeaderChapter.GetAsync());

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.DateTime >= getDt &&
                            w.CountryId != Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") && // غیر ایرانی
                            w.CountryId != Guid.Parse("c219de1c-00cd-4195-aad0-fff47b44401e") && // غیر هندی
                            w.CountryId != Guid.Parse("68824967-89a1-491d-a68c-ae8451d6c389") && // غیر ترکی

                            w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5")) // سینمایی
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek,
                    CapterCount = getHeaderChapter.Count(c => c.MultimediaHeaderId == s.Id)
                })
                .OrderByDescending(o => o.Num).Take(20);

            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        public async Task LstSinemaKharejiOld(long userId)
        {
            var getDt = DateTime.Now.AddDays(-15);
            var getHeaderChapter = (await _multiHeaderChapter.GetAsync());

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.DateTime < getDt &&
                            w.CountryId != Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") && // غیر ایرانی
                            w.CountryId != Guid.Parse("c219de1c-00cd-4195-aad0-fff47b44401e") && // غیر هندی
                            w.CountryId != Guid.Parse("68824967-89a1-491d-a68c-ae8451d6c389") && // غیر ترکی

                            w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5")) // سینمایی
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek,
                    CapterCount = getHeaderChapter.Count(c => c.MultimediaHeaderId == s.Id)
                })
                .OrderByDescending(o => o.Num).Take(20);

            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        public async Task LstSinemaIraniOld(long userId)
        {
            var getDt = DateTime.Now.AddDays(-7);
            var getHeaderChapter = (await _multiHeaderChapter.GetAsync());

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.DateTime < getDt &&
                            w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") &&
                            w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5"))
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek,
                    CapterCount = getHeaderChapter.Count(c => c.MultimediaHeaderId == s.Id)
                }).Take(20).OrderByDescending(o => o.Num);

            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        public async Task LstTorki(long userId)
        {
            var getHeaderChapter = (await _multiHeaderChapter.GetAsync());

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.CountryId == Guid.Parse("68824967-89a1-491d-a68c-ae8451d6c389"))
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek,
                    CapterCount = getHeaderChapter.Count(c => c.MultimediaHeaderId == s.Id)
                })
                .OrderByDescending(o => o.Num);

            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        public async Task LstHindi(long userId)
        {
            var getHeaderChapter = (await _multiHeaderChapter.GetAsync());

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.CountryId == Guid.Parse("c219de1c-00cd-4195-aad0-fff47b44401e"))
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek,
                    MultimediaGroup =
                    (s.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5") ? "سینمایی" : "سریال"),
                    CapterCount = getHeaderChapter.Count(c => c.MultimediaHeaderId == s.Id)
                })
                .OrderByDescending(o => o.Num);

            var get = string.Join(Environment.NewLine, getListFilm.Select(s => $" {s.NamePersian}"));
            // var get = string.Join(Environment.NewLine, getListFilm.Select(s => $"{s.MultimediaGroup} - {s.NamePersian}"));

            await bot.SendContent(userId, get);
        }
        public async Task LstSerialIKhareji(long userId)
        {
            var getHeaderChapter = (await _multiHeaderChapter.GetAsync());

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.CountryId != Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") && w.DayOfWeek == 7)
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek,
                    CapterCount = getHeaderChapter.Count(c => c.MultimediaHeaderId == s.Id)
                })
                .OrderByDescending(o => o.Num).Take(20);


            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian + "  -  (" + s.CapterCount + " فصل)"));
            await bot.SendContent(userId, get);
        }
        public async Task LstSerialIraniFinish(long userId)
        {
            //w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5")
            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") &&
                            w.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef") &&
                            w.DayOfWeek == 7)
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num,
                    s.DayOfWeek
                })
                .OrderByDescending(o => o.Num).Take(25);
            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        public async Task LstSerialIraniNew(long userId)
        {
            var modelWeekOfDays = ClassDateTime.FillDayOfWeek(new ClassDateTime.ModelWeekOfDay
            {
                Code = 7,
                Fa = "پایان",
                En = "Finish"
            });

            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") && w.DayOfWeek != 7)
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country, 
                    s.Num,
                    s.DayOfWeek
                })
                .OrderBy(o => o.DayOfWeek);
            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian + " => " + modelWeekOfDays.First(f => f.Code == s.DayOfWeek).Fa));
            await bot.SendContent(userId, get);
        }

        public async Task LstSinemaAnimation(long userId)
        {
            //w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5")
            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => w.IsAnimated && !w.IsAnime)
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country, 
                    s.Num
                })
                .OrderByDescending(o => o.Num).Take(20);
            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        public async Task LstSinemaAnime(long userId)
        {
            //w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5")
            var getListFilm = (await _multiHeader.GetByModel())
                .Where(w => !w.IsAnimated && w.IsAnime)
                .Select(s => new
                {
                    s.Id,
                    s.NamePersian,
                    //s.Country,
                    s.Num
                })
                .OrderByDescending(o => o.Num).Take(20);
            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            await bot.SendContent(userId, get);
        }
        #endregion

        private async void Bot_OnCallbackReceived(long userId, string callbackData)
        {
            // نمایش در UI (Thread-safe)
            if (InvokeRequired)
            {
                Invoke(new Action(() => Bot_OnCallbackReceived(userId, callbackData)));
                return;
            }

            //  lblStatus.Text = $"🖱️ کلیک از {userId}: {callbackData}";

            switch (callbackData)
            {
                case "iranian_list":
                    // ✅ نمایش لیست ایرانی
                    //await bot.SendContent(userId, "در حال آماده سازی لیست ...");
                    await ShowIranianList(userId);
                    break;

                case "foreign_list":
                    // ✅ نمایش لیست خارجی
                    //await bot.SendContent(userId, "در حال آماده سازی لیست ...");
                    await ShowForeignList(userId);
                    break;

                default:
                    await bot.SendContent(userId, "❌ گزینه نامعتبر است.");
                    break;
            }
        }

        public async Task ShowIranianList(long userId)
        {
            var getListFilm = (await _multiHeader.GetByModel()).Select(s => new
            {
                s.Id,
                s.NamePersian,
                //s.Country,
                s.Num
            }).ToList();
            //var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            var maxBy = getListFilm.MaxBy(s => s.Num);
            var get = maxBy.NamePersian;
            var getImage = await _multiHeader.LocationForImage(maxBy.Id);
            //var get = string.Join(Environment.NewLine, getListFilm.MaxBy(s => s.Num));
            //await bot.SendMessage(userId, get);
            var mediaUrl = Path.Combine(getImage, maxBy.Num.ToString(), "Image.jpg");
            await bot.SendContent(userId, get, mediaUrl, true);

        }
        public async Task ShowForeignList(long userId)
        {
            var getListFilm = (await _multiHeader.GetByModel()).Where(w => w.CountryId == Guid.Parse("fc4bc08a-bdf1-4b97-9759-75dca23de1f5")).Select(s => new
            {
                s.Id,
                s.NamePersian,
                //s.Country,
                s.Num
            }).ToList();
            var get = string.Join(Environment.NewLine, getListFilm.Select(s => s.NamePersian));
            //var maxBy = getListFilm.MaxBy(s => s.Num);
            //var get = maxBy.NamePersian;
            //var getImage = await _multiHeader.LocationForImage(maxBy.Id);
            //var get = string.Join(Environment.NewLine, getListFilm.MaxBy(s => s.Num));
            await bot.SendMessage(userId, get);
            //var mediaUrl = Path.Combine(getImage, maxBy.Num.ToString(), "Image.jpg");
            //await bot.SendContent(userId, get, mediaUrl, true);

        }
        private async Task PollMessages()
        {
            await bot.CheckForMessages();
        }

        // وقتی پیام جدیدی می‌رسد
        private void Bot_OnMessageReceived(MessageInfo msg)
        {
            // نمایش در UI (Thread-safe)
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Bot_OnMessageReceived(msg);
                }));
                return;
            }

            //txtUserId.Text = msg.UserId.ToString();
            //txtLastMessage.Text = msg.Text;
            //lstMessages.Items.Add($"[{msg.Date:HH:mm:ss}] {msg.UserName}: {msg.Text}");
            //lblStatus.Text = $"📩 پیام از {msg.UserName} دریافت شد.";
        }

        // وقتی خطایی رخ می‌دهد
        private void Bot_OnError(string error)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Bot_OnError(error)));
                return;
            }

            //  lblStatus.Text = $"❌ خطا: {error}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }



        private async void btnPrice_Click(object sender, EventArgs e)
        {
            await bot.SendPrice();
        }
    }
}
