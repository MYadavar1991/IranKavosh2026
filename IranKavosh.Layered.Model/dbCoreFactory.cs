using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IranKavosh.Layered.Model
{
    public class dbCoreFactory : IDesignTimeDbContextFactory<dbCore>
    {
        public dbCore CreateDbContext(string[] args)
        {
            // بارگذاری تنظیمات از appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // مسیر اجرای دستور EF
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<dbCore>();
            optionsBuilder.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly("IranKavosh.Layered.WebAPI") // محل ذخیره migration
            );

            return new dbCore(optionsBuilder.Options);
        }
    }
}