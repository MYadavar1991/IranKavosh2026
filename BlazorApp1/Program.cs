using BlazorApp1.Components;
using BlazorApp1.HttpClient;
using IranKavosh.Layered.Service.Services;
using Microsoft.Extensions.FileProviders;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSyncfusionBlazor();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://192.168.1.160:1212/api/";
var apiTimeout = TimeSpan.FromSeconds(int.Parse(builder.Configuration["ApiTimeoutSeconds"] ?? "10"));

// ثبت همه سرویس‌ها با یک خط
builder.Services.AddApiHttpClient<MultimediaHeaderHttpService>(apiBaseUrl, apiTimeout);
builder.Services.AddApiHttpClient<MultimediaHeaderChapterHttpService>(apiBaseUrl, apiTimeout);
builder.Services.AddApiHttpClient<MultimediaChapterDetailHttpService>(apiBaseUrl, apiTimeout);
builder.Services.AddApiHttpClient<MultiLocationHttpService>(apiBaseUrl, apiTimeout);
builder.Services.AddApiHttpClient<MultiGenreHttpService>(apiBaseUrl, apiTimeout);
builder.Services.AddApiHttpClient<MultiCountryHttpService>(apiBaseUrl, apiTimeout);
// سرویس‌های دیگر را هم همینطور اضافه کنید

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(@"U:\__VideoTest"),
//    RequestPath = "/Img"
//});
app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
