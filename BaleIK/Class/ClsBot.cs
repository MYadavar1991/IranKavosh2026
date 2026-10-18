using BaleBotNet;
using BaleBotNet.Enums;
using BaleBotNet.Methods;
using BaleBotNet.Types;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using IranKavosh.Layered.Model.Table_Bot;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCom.Class;
using File = BaleBotNet.Types.File;
using Message = BaleBotNet.Types.Message;
using Timer = System.Timers.Timer;

namespace BaleIK.Class
{

    public class MessageInfo
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }

    public class ClsBot
    {
        private static readonly HttpClient _httpClient = new();
        private readonly PersonalHttpService _personal = new(_httpClient);
        private readonly ChanelMemberHttpService _chanelMember = new(_httpClient);

        private BaleBotClient bot;
        private string botToken = "1723187126:DU8S4HAsbBK2II1RGcFFPbygzwUrh8BWMvQ";

        // آخرین شناسه پردازش شده
        private int lastUpdateId = 0;

        // ذخیره زمان آخرین منوی ارسال شده برای هر کاربر
        private Dictionary<long, DateTime> lastMenuSent = new Dictionary<long, DateTime>();

        public event Action<MessageInfo> OnMessageReceived;
        public event Action<string> OnError;
        // اضافه کنید به قسمت رویدادهای کلاس ClsBot
        public event Action<long, string> OnCallbackReceived;  // (UserId, CallbackData)
        public ClsBot()
        {
            bot = new BaleBotClient(botToken);
           // StartMembershipChecker();
        }



    

        private HashSet<int> processedUpdateIds = new HashSet<int>();

        public async Task<bool> IsUserInChannel(long userId)
        {
            var chatMember = await bot.GetChatMember(getCID, userId);
            if (chatMember != null)
            {
                if (chatMember.Status == ChatMemberStatus.Member||
                    chatMember.Status == ChatMemberStatus.Administrator ||
                    chatMember.Status == ChatMemberStatus.Creator)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task CheckForMessages()
        {
            try
            {
                // فقط آپدیت‌های جدیدتر از lastUpdateId را بگیر
                var updates1 = await bot.GetUpdates();
                var updates = await bot.GetUpdates(offset: lastUpdateId + 1);

                if (updates == null || updates.Length == 0)
                    return;

                foreach (var update in updates)
                {
                    // ✅ اگر این آپدیت قبلاً پردازش شده، نادیده بگیر
                    if (processedUpdateIds.Contains(update.UpdateId))
                        continue;

                    // ذخیره شناسه به عنوان پردازش شده
                    processedUpdateIds.Add(update.UpdateId);

                    // محدود کردن حجم HashSet (اختیاری، برای جلوگیری از بزرگ شدن بیش از حد)
                    if (processedUpdateIds.Count > 1000)
                    {
                        processedUpdateIds.Clear();
                    }

                    // به‌روزرسانی lastUpdateId
                    if (update.UpdateId > lastUpdateId)
                        lastUpdateId = update.UpdateId;

                    // پردازش پیام متنی
                    if (update.Message != null)
                    {
                        await HandleMessage(update.Message);
                    }

                    ////// پردازش کلیک روی دکمه
                    if (update.CallbackQuery != null)
                    {
                        await HandleCallback(update.CallbackQuery);
                    }
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }
        ChatId getCID;
        string getRef = "";
        private async Task HandleMessage(Message message)
        {
            getCID = message.Chat;
            var msgInfo = new MessageInfo
            {
                UserId = message.Chat.Id,
                UserName = message.Chat.FirstName ?? "کاربر",
                Text = message.Text ?? "",
                Date = DateTime.Now
            };
           
            if (message.Text != null && message.Text.Contains("/start ref"))
            {
               
                getRef = message.Text.Replace("/start ref_", "");
                //await _chanelMember.AddReferral(new ModelBot.ModelReferral
                //{
                //    ReferralSend = msgInfo.UserId,
                //    ReferralReceive = getRef.GetNum<long>(),

                //});
               
            }
            else
            {
                //await _chanelMember.AddReferral(new ModelBot.ModelReferral
                //{
                //    ReferralSend = 0,
                //    ReferralReceive = msgInfo.UserId,

                //});
            }
            if (message.Contact != null)
            {
                //string phoneNumber = message.Contact.PhoneNumber;
               

                string userName = message.Chat.Username;
               
                var personal = new Personal
                {
                    BotUserId = msgInfo.UserId,
                    FirstName = message.Contact.FirstName,
                    LastName = message.Contact.LastName,
                    BotLoginDateTime = DateTime.Now,
                    Username = userName,
                    Mobile = "0" + message.Contact.PhoneNumber.Substring(2, 10),

                    PersonalGroupId = Guid.Parse("8168205e-2218-43bc-95aa-b53dde855bb4"),
                };
                await _personal.AddFromBotAsync(personal);
                if (getRef!="")
                {
                    await _chanelMember.AddReferral(new ModelBot.ModelReferral
                    {
                        ReferralSend = getRef.GetNum<long>(),
                        ReferralReceive = msgInfo.UserId,
                        //ReferralReceive = getRef.GetNum<long>(),

                    });
                    getRef = "";
                }
              
                
                await bot.SendMessage(
                    chatId: message.Chat.Id.ToString(),
                    text: $"✅ عضویت شما با موفقیت ثت شد"
                );
                await Menu_Main(message.Chat.Id);
                return;
            }
            // فقط دستور /start را پردازش کن
            if (msgInfo.Text == "/start")
            {
                // بررسی نکن چند ثانیه گذشته، فقط اگر در این ثانیه منو نفرستاده
                if (!lastMenuSent.ContainsKey(msgInfo.UserId) || (DateTime.Now - lastMenuSent[msgInfo.UserId]).TotalSeconds > 2)
                {
                    #region بررسی عصویت 

                    var getAny = await _personal.BotAnyUserId(msgInfo.UserId);
                    if (getAny)
                    {
                        await Menu_Main(msgInfo.UserId);
                    }

                    #endregion
                    else
                    {
                        var getStatus = await AllowContact(msgInfo.UserId);
                        if (getStatus)
                        {

                            await Menu_Main(msgInfo.UserId);
                        }
                    }

                    lastMenuSent[msgInfo.UserId] = DateTime.Now;
                }
            }
            else
            {
                OnTextCommand?.Invoke(msgInfo.UserId, msgInfo.Text);
            }
            //else if (msgInfo.Text == "سریال های ایرانی جدید")
            //{

            //}


            OnMessageReceived?.Invoke(msgInfo);
        }
        public event Action<long, string> OnTextCommand;  // (UserId, Text)
        //private async Task HandleCallback(CallbackQuery callback)
        //{
        //    long userId = callback.From.Id;
        //    string data = callback.Data;

        //    switch (data)
        //    {
        //        case "iranian_list":
        //            await bot.SendMessage(userId.ToString(), "📰 لیست ایرانی\n\nدر حال آماده‌سازی... به زودی ارائه می‌شود.");
        //            break;
        //        case "foreign_list":
        //            await bot.SendMessage(userId.ToString(), "🌍 لیست خارجی\n\nدر حال آماده‌سازی... به زودی ارائه می‌شود.");
        //            break;
        //        default:
        //            await bot.SendMessage(userId.ToString(), "❌ گزینه نامعتبر است.");
        //            break;
        //    }

        //    // ✅ اصلاح شده: AnswerCallbackQuery با دو پارامتر الزامی
        //    await bot.AnswerCallbackQuery(
        //        callbackQueryId: callback.Id,  // شناسه کلیک
        //        text: null                      // پیام اختیاری
        //    );

        //}
        private async Task HandleCallback(CallbackQuery callback)
        {
            long userId = callback.From.Id;
            string data = callback.Data;

            // ✅ ارسال رویداد به فرم (و پاسخ به کلیک در فرم داده می‌شود)
            OnCallbackReceived?.Invoke(userId, data);

            // فقط پاسخ به کلیک برای بستن حالت بارگذاری
            await bot.AnswerCallbackQuery(callback.Id, "CallBack");
        }



  

        public async Task<bool> AllowContact(long userId)
        {
            try
            {
                var contact = new ReplyKeyboardMarkup
                {
                    Keyboard =
                    [
                        [
                            new KeyboardButton { Text = "📱 ثبت عضویت",RequestContact = true},
                        ],

                    ],
                    // ResizeKeyboard = true  // اندازه کیبورد رو کوچیک می‌کنه

                    //ResizeKeyboard = true,
                    //OneTimeKeyboard = true  // بعد از استفاده، کیبورد مخفی شود
                };
                var getStatus = await bot.SendMessage(
                     chatId: userId.ToString(),
                     text: "👋 خوش آمدید! لطفاً هویت خود را تایید نمایید:",
                     replyMarkup: contact
                 );

                return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        //public async Task<bool> SendMainMenu(long userId)
        //{

        //    try
        //    {
        //        var keyboard = new ReplyKeyboardMarkup
        //        {
        //            Keyboard =
        //            [
        //                [  // ✅ ردیف جدید برای لینک دعوت
        //                    new KeyboardButton { Text = "🎁 لینک دعوت و جوایز" }
        //                ],
        //                [
        //                    new KeyboardButton { Text = "سریال های ایرانی جدید" },
        //                    new KeyboardButton { Text = "سینمایی ایرانی جدید" },
        //                ],
        //                [
        //                    new KeyboardButton { Text = "سریال های ایرانی" },
        //                    new KeyboardButton { Text = "سینمایی ایرانی" }
        //                ],
        //                [
        //                    new KeyboardButton { Text = "سریال های خارجی" },
        //                    new KeyboardButton { Text = "سینمایی خارجی" },

        //                    new KeyboardButton { Text = "فیلم هندی" },
        //                    new KeyboardButton { Text = "فیلم ترکی" },
        //                ],
        //                [
        //                    new KeyboardButton { Text = "انیمیشن" },
        //                    new KeyboardButton { Text = "انیمه" }
        //                ],

        //            ],
        //            // ResizeKeyboard = true  // اندازه کیبورد رو کوچیک می‌کنه
        //        };




        //        //var keyboard = new InlineKeyboardMarkup
        //        //{
        //        //    InlineKeyboard = new InlineKeyboardButton[][]
        //        //    {
        //        //        new InlineKeyboardButton[]
        //        //        {
        //        //            new InlineKeyboardButton { Text = $"سریال های ایرانی جدید", CallbackData = "iranian_list" },
        //        //            new InlineKeyboardButton { Text = "سریال های ایرانی تمام شده", CallbackData = "iranian_list2" },

        //        //        },
        //        //        new InlineKeyboardButton[]
        //        //        {

        //        //            new InlineKeyboardButton { Text = "سریال های خارجی", CallbackData = "foreign_list" }
        //        //        }
        //        //    }
        //        //};

        //        await bot.SendMessage(
        //            chatId: userId.ToString(),
        //            text: "👋 خوش آمدید! لطفاً یکی از گزینه‌های زیر را انتخاب کنید:",
        //            replyMarkup: keyboard
        //        );
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        OnError?.Invoke($"خطا در ارسال منو: {ex.Message}");
        //        return false;
        //    }
        //}

        #region مدیریت منو ها
        public async Task<bool> Menu_Main(long userId)
        {

            try
            {
                var keyboard = new ReplyKeyboardMarkup
                {
                    Keyboard =
                    [
                        [
                            new KeyboardButton { Text = "🎁 لینک دعوت و جوایز" }
                        ],
                        [
                            new KeyboardButton { Text = "فیلم ها" },
                            //new KeyboardButton { Text = "دوربین مداربسته" },
                            new KeyboardButton { Text = "دوربین مداربسته (به زودی)" },
                        ],
                        [
                            //new KeyboardButton { Text = "مینی اپ" },
                            new KeyboardButton { Text = "طراحی" },
                            //new KeyboardButton { Text = "طراحی (به زودی)" },
                            new KeyboardButton { Text = "🔑 کلمه عبور و نام کاربری" }
                        ],

                    ],

                };

             
            
                await bot.SendMessage(
                    chatId: userId.ToString(),
                    text: "👋 خوش آمدید! لطفاً یکی از گزینه‌های زیر را انتخاب کنید:",
                    replyMarkup: keyboard
                    
                );
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"خطا در ارسال منو: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> Menu_Video(long userId)
        {

            try
            {
                var keyboard = new ReplyKeyboardMarkup
                {
                    Keyboard =
                    [
                        [
                            new KeyboardButton { Text = "🎁 لینک دعوت و جوایز" }
                        ],
                        [
                            new KeyboardButton { Text = "ایرانی" },
                            new KeyboardButton { Text = "خارجی" },
                            new KeyboardButton { Text = "هندی" },
                            new KeyboardButton { Text = "ترکی" },
                        ],
                        [
                            new KeyboardButton { Text = "انیمیشن" },
                            new KeyboardButton { Text = "انیمه" }
                        ],
                        [
                            new KeyboardButton { Text = "بازگشت" },
                         
                        ],

                    ],

                };



                await bot.SendMessage(
                    chatId: userId.ToString(),
                    text: "👇",
                    replyMarkup: keyboard
                );
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"خطا در ارسال منو: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> Menu_Video_After(long userId,string title)
        {
                
            try
            {
                var keyboard = new ReplyKeyboardMarkup
                {
                    Keyboard =
                    [
                        [
                            new KeyboardButton { Text = "🎁 لینک دعوت و جوایز" }
                        ],
                        [
                            new KeyboardButton { Text = $"سریال های در حال پخش ({title})" },
                            new KeyboardButton { Text = $"سینمایی جدید ({title})" },
                        ],
                        [
                            new KeyboardButton { Text = $"سریال تمام شده ({title})" },
                            new KeyboardButton { Text = $"سینمایی قدیمی ({title})" },
                        ],
                    
                        [
                            new KeyboardButton { Text = "بازگشت به فیلم"},

                        ],
                    ],

                };



                await bot.SendMessage(
                    chatId: userId.ToString(),
                    text: "👇",
                    replyMarkup: keyboard
                );
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"خطا در ارسال منو: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> Menu_Desine(long userId)
        {

        //   await bot.SendPhoto(userId.ToString(), "https://zertwo.files.ir/filesir/uploads/49e55086-1161-4fba-822a-b3f16d6a0fa5/49e55086-1161-4fba-822a-b3f16d6a0fa5?response-content-type=image%2Fjpeg&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=BNzxtCrjGkJ5n8xyYI9o%2F20260530%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20260530T093608Z&X-Amz-SignedHeaders=host&X-Amz-Expires=1800&X-Amz-Signature=0dc3ea0294a45f7a15ee2562d7184daa7769ca1542f2510fd6153c07f3c93a9d");
          //  return true;
            try
            {
                var keyboard = new ReplyKeyboardMarkup
                {
                    Keyboard =
                    [
                        [
                            new KeyboardButton { Text = "🎁 لینک دعوت و جوایز" }
                        ],
                        [
                            new KeyboardButton { Text = "کارت ویزیت" },
                            new KeyboardButton { Text = "لوگو" },
                            new KeyboardButton { Text = "تراکت" },
                        ],
                        [
                            new KeyboardButton { Text = "بنر" },
                            new KeyboardButton { Text = "ترحیم" }
                        ],
                        [
                            new KeyboardButton { Text = "بازگشت" },
                        ],
                    ],
                };
                


                await bot.SendMessage(
                    chatId: userId.ToString(),
                    text: "👇",
                    replyMarkup: keyboard
                );
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"خطا در ارسال منو: {ex.Message}");
                return false;
            }
        }
        #endregion

        public async Task<BaleBotClient> SendContent(long userId, string message, string mediaUrl = null, bool isPhoto = false)
        {
            try
            {
                if (isPhoto && !string.IsNullOrEmpty(mediaUrl))
                {
                    return await SendPhoto(userId, message, mediaUrl);
                }
                else
                {
                    return await SendMessage(userId, message);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
                return null;
            }
        }
        public async Task<BaleBotClient> SendMessage(long userId, string message)
        {
            try
            {
                await bot.SendMessage(userId.ToString(), message);
                return bot;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
                return null;
            }
        }
        public async Task<BaleBotClient> SendPhoto(long userId, string caption, string uriImg)
        {
            try
            {
                await bot.SendPhoto(
                    chatId: userId,
                    fileInfo: new FileInfo(uriImg),
                    fileName: System.IO.Path.GetFileName(uriImg),
                    caption: caption  // استفاده از caption واقعی
                );
                return bot;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
                return null;
            }
        }

        #region درخواست پول

        public async Task<BaleBotClient> SendPrice()
        {
            var getStatus= await bot.SendInvoice(getCID, "تست خرید", "توضیحات", "payLoad", "WALLET-TEST-1111111111111111", new[] { new LabeledPrice { Amount = 10000, Label = "Lable 1" } });
       //     return /*true*/;
       return null;
        }

        #endregion
        #region دریافت مشخصات کاربر

        // ارسال دکمه درخواست شماره موبایل
        //public async Task<bool> SendRequestContactButton(long userId, string text)
        //{
        //    try
        //    {
        //        var keyboard = new ReplyKeyboardMarkup
        //        {
        //            Keyboard = new List<List<KeyboardButton>>
        //            {
        //                new List<KeyboardButton>
        //                {
        //                    new KeyboardButton
        //                    {
        //                        Text = "📱 ارسال شماره موبایل",
        //                        RequestContact = true  // 🔑 کلید اصلی
        //                    }
        //                }
        //            },
        //            ResizeKeyboard = true,
        //            OneTimeKeyboard = true  // بعد از استفاده، کیبورد مخفی شود
        //        };

        //        await bot.SendMessage(
        //            chatId: userId.ToString(),
        //            text: text,
        //            replyMarkup: keyboard
        //        );
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        OnError?.Invoke(ex.Message);
        //        return false;
        //    }
        //}

        #endregion
    }
}