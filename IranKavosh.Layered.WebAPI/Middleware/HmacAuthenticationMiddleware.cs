using System.Security.Cryptography;
using System.Text;

namespace IranKavosh.Layered.WebAPI.Middleware;

public class HmacAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;

    public HmacAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
       // _secretKey = configuration["Hmac:SecretKey"];
        _secretKey = "KeyApiIranKavosh2025";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-Hmac-Signature", out var signature))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing HMAC signature.");
            return;
        }

        // نمونه ساده: فقط مسیر و متد را امضا می‌کند
       // var data = $"{context.Request.Method}:{context.Request.Path}";
        var data = $"{context.Request.Path}";
        var computedSignature = ComputeHmac(data, _secretKey);

        if (signature != computedSignature)
        {
            context.Response.StatusCode = 410;
            await context.Response.WriteAsync("Invalid HMAC signature.");
            return;
        }

        await _next(context);
    }

    private string ComputeHmac(string data, string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var dataBytes = Encoding.UTF8.GetBytes(data);
        using var hmac = new HMACSHA256(keyBytes);
        var hash = hmac.ComputeHash(dataBytes);
        return Convert.ToBase64String(hash);
    }
}