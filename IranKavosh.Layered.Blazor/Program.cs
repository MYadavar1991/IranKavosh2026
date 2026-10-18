using IranKavosh.Layered.Blazor.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Extensions.Head;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddAntiforgery(); // Already present, just for context
builder.Services.AddSyncfusionBlazor();
var app = builder.Build();

// Add CSP header
//app.Use(async (context, next) =>
//{
//    // context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self' 'unsafe-inline' 'unsafe-eval'; style-src 'self' 'unsafe-inline';");
//    context.Response.Headers.Add("Content-Security-Policy",
//        "default-src 'self'; " +
//        "script-src 'self' 'unsafe-inline' 'unsafe-eval'; " +
//        "style-src 'self' 'unsafe-inline'; " +
//        "font-src 'self' data: https:; " +
//        "connect-src 'self'  http://localhost:5222 wss:; " +
//        "img-src 'self' data: https:;");


//    await next();
//});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();