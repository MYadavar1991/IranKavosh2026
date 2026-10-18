using IranKavosh.Layered.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IranKavosh.Layered.Service.Middleware;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IranKavosh.Layered.Service.HttpBase
{
    public abstract class HttpServiceBase
    {

        protected readonly HttpClient Client;

        protected HttpServiceBase(HttpClient client)
        {
            if (client.BaseAddress == null)
            {
                //client.BaseAddress = new Uri("http://localhost:5140/api/");
                client.BaseAddress = new Uri("http://192.168.1.160:1212/api/");
                client.Timeout = TimeSpan.FromSeconds(15);

            }
            Client = client;
        }
        public async Task<TResponse> SendRequestAsync<TResponse>(
            string url,
            HttpMethod method,
            object? body = null)
        {
            try
            {
                var headers = PrepareHmacHeader(url);
                var request = new HttpRequestMessage(method, url);

                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

                if (body != null)
                {
                    if (body is bool)
                    {
                        var rawJson = JsonSerializer.Serialize(body);
                        request.Content = new StringContent(rawJson, Encoding.UTF8, "application/json");
                    }
                    else
                    {
                        var rawJson = JsonSerializer.Serialize(body);
                        var wrappedJson = JsonSerializer.Serialize(rawJson);
                        request.Content = new StringContent(wrappedJson, Encoding.UTF8, "application/json");
                    }
                }

                var response = await Client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    //   throw new HttpRequestException($"خطا در پاسخ سرور: {response.StatusCode} - {errorContent}");
                }

                var responseJson = await response.Content.ReadAsStringAsync();

                var deserialize =
                    JsonConvert.DeserializeObject<TResponse>(responseJson, ClsJason.IsoDateTimeConverter2());
                //  if (deserialize == null)
                //   throw new JsonException("پاسخ دریافتی قابل تبدیل به مدل مورد نظر نیست.");

                return deserialize;
            }
            catch (Exception e)
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogWin");
                if (!Directory.Exists(logPath))
                    Directory.CreateDirectory(logPath);

                string fileName = $"error_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
                string fullPath = Path.Combine(logPath, fileName);

                var sb = new StringBuilder();
                sb.AppendLine($"Exception Source: {e}");
                sb.AppendLine($"-------------");
                sb.AppendLine($"Message: {e.Message}");
                await File.WriteAllTextAsync(fullPath, sb.ToString());
                // Console.WriteLine(e);
                // throw;
            }

            return default;
        }


        protected Dictionary<string, string> PrepareHmacHeader(string relativeUrl)
        {
            // چون BaseAddress شامل /api هست، باید مسیر کامل رو بسازیم
            var fullPath = $"/api/{relativeUrl}";
            var secretKey = "KeyApiIranKavosh2025";
            var signature = ComputeHmac(fullPath, secretKey);

            return new Dictionary<string, string>
            {
                { "X-Hmac-Signature", signature }
            };
        }

        string ComputeHmac(string path, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var dataBytes = Encoding.UTF8.GetBytes(path);
            using var hmac = new HMACSHA256(keyBytes);
            var hash = hmac.ComputeHash(dataBytes);
            return Convert.ToBase64String(hash);
        }

    }
}
