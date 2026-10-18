using System.Net;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace EitaaIK
{
    public class EitaaBot
    {
        private static readonly string token = "bot509050:afac28c6-165c-4515-b68e-6615c925cea7";
        private static readonly string chatId = "11209423";
        private static readonly string apiUrl = "https://eitaayar.ir/api/";

        //public static void SendText()
        //{
        //    string url = $"{apiUrl}{token}/sendFile";
        //    // initialise the curl request
        //    $request = curl_init('https://eitaayar.ir/api/'.$token.'/sendFile');

        //    // send a file
        //    curl_setopt($request, CURLOPT_POST, true);
        //}
        public static async Task<bool> SendFileAsync(string caption, string title, string? filePath = null)
        {
            try
            {
                // ساخت URL با تمام پارامترها (مشابه آدرس مثال)
                string url = $"{apiUrl}{token}/sendMessage?" +
                             $"chat_id={chatId}" +
                             $"&text={Uri.EscapeDataString(title)}" + // متن را برای URL encode می‌کند
                             $"&date=0" +
                             $"&parse_mode=" +
                             $"&viewCountForDelete=";

                using (var client = new WebClient())
                {
                    // ارسال درخواست GET
                    string response = client.DownloadString(url);

                    Console.WriteLine($"📨 پاسخ سرور: {response}");

                    // بررسی پاسخ
                    //var json = JObject.Parse(response);
                    //if (json["ok"]?.Value<bool>() == true)
                    //{
                    //    Console.WriteLine("✅ پیام با موفقیت ارسال شد!");
                    //    // می‌توانید message_id را هم بخوانید
                    //    var messageId = json["result"]?["message_id"]?.Value<int>();
                    //    Console.WriteLine($"📝 شناسه پیام: {messageId}");
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"❌ خطا: {json["description"]?.Value<string>()}");
                    //}
                }
            }
            catch (WebException ex) // خطاهای شبکه را بگیرید
            {
                Console.WriteLine($"❌ خطای شبکه: {ex.Message}");
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    using (var reader = new System.IO.StreamReader(errorResponse.GetResponseStream()))
                    {
                        string errorText = reader.ReadToEnd();
                        Console.WriteLine($"📄 جزئیات خطا: {errorText}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطا: {ex.Message}");
            }

            return false;
        }
    }
}
