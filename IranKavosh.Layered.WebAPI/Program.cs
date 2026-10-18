using IranKavosh.Layered.Model;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Access;
using IranKavosh.Layered.WebAPI.ListControllers;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// ثبت سرویس‌ها

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new IKAccess());
    })
    .AddJsonOptions(json =>
    {
        json.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UnitOfWork>();
var getdata = ClassController.GetController();

//builder.Services.AddDbContext<dbCore>();
builder.Services.AddDbContext<dbCore>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");
if (Directory.Exists(@"C:\TempDownloadPages"))
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(@"C:\TempDownloadPages"),
        RequestPath = "/download-pages"
    });
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(@"U:\"),
//    RequestPath = "/DrvU"
//});
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(@"U:\"),
//    RequestPath = "/Img2"
//});
//app.UseMiddleware<HmacAuthenticationMiddleware>(); // احراز هویت قبل از هر درخواستی

// تنظیمات محیط توسعه
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
// ASP.NET Core 6+


// بعد از AddControllers
app.UseCors("AllowAll");
//}

// میان‌افزارها
app.UseHttpsRedirection();
app.UseAuthorization();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(@"E:\Video4\3694"),
//    RequestPath = "/KA",
//});
app.MapControllers();
app.Run();