using IranKavosh.Layered.Model.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Channels;
using IranKavosh.Layered.Model.Table_Bot;
using IranKavosh.Layered.Model.Tables.BOT;

namespace IranKavosh.Layered.Model
{
    public class dbCore : DbContext
    {
        public dbCore(DbContextOptions<dbCore> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // سایر تنظیمات مدل‌ها
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder op)
        //{
        //    op.UseSqlServer("Data Source = KVServer" +
        //                    ";Initial Catalog = IKCore2" +
        //                    ";Integrated Security = false" +
        //                    ";MultipleActiveResultSets = true" +
        //                    ";TrustServerCertificate = True" +
        //                    ";User ID = sa" +
        //                    ";Password = Moaz1370110");
        //    op.EnableSensitiveDataLogging(true);
        //    base.OnConfiguring(op);
        //}

        //پرسنل
        //گزارش گیری
        //لاگین
        //وود و خروج


        #region Table Model

        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        //public virtual DbSet<SalesService> SalesServices { get; set; }



        /// <summary>
        /// پرسنل
        /// </summary>
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Personal> Personals { get; set; }
        public virtual DbSet<PersonalGroup> PersonalGroups { get; set; }
        /// <summary>
        /// سهام داران
        /// </summary>
        public virtual DbSet<ShareHolderHeader> ShareHolderHeaders { get; set; }
        public virtual DbSet<ShareHolderDetail> ShareHolderDetails { get; set; }

        /// <summary>
        /// حساب معین
        /// ---
        /// Debtor - Credetor
        /// </summary>
        public virtual DbSet<DefinitiveAccount> DefinitiveAccounts { get; set; }

        #region صندوق

        public virtual DbSet<Fund> Funds { get; set; }
        public virtual DbSet<FundType> FundTypes { get; set; }
        public virtual DbSet<BuySellDetail> BuySellDetails { get; set; }
        public virtual DbSet<PriceList> PriceLists { get; set; }
        
        public virtual DbSet<PriceListDiscount> PriceListDiscounts { get; set; }

        #endregion

     



        #region Movies



        //chapter
        public virtual DbSet<MultimediaHeader> MultimediaHeaders { get; set; }
        public virtual DbSet<MultimediaHeaderChapter> MultimediaHeaderChapters { get; set; }
        public virtual DbSet<MultimediaChapterDetail> MultimediaChapterDetails { get; set; }

        /// <summary>
        /// بازیگران
        /// </summary>
        public virtual DbSet<MultiActor> MultiActors { get; set; }
        /// <summary>
        /// کودکان
        /// جوانان
        /// بزرگ سال
        /// </summary>
        public virtual DbSet<MultiAge> MultiAges { get; set; }
        /// <summary>
        /// ایرانی - آمریکایی
        /// سایر
        /// هندی
        /// کره ای
        /// ترکی
        /// ژاپن
        ///  </summary>
        public virtual DbSet<MultiCountry> MultiCountries { get; set; }
        public virtual DbSet<MultiLocation> MultiLocations { get; set; }
        public virtual DbSet<MultiGroup> MultiGroups { get; set; }
        /// <summary>
        /// قیمت گذاری
        /// </summary>
        public virtual DbSet<MultiPrice> Prices { get; set; }

        /// <summary>
        /// ژانرها
        /// </summary>
        public virtual DbSet<MultiGenre> MultiGenres { get; set; }
        /// <summary>
        /// ثبت ژانرها
        /// </summary>
      //  public virtual DbSet<MultiGenreList> MultiGenreLists { get; set; }
        /// <summary>
        /// لیست خرید فیلم توسط مشتری
        /// </summary>
        public virtual DbSet<MultiListPersonal> MultiListPersonals { get; set; }
        public virtual DbSet<MultiSellType> MultiSellTypes { get; set; }
        public virtual DbSet<MultiChapterType> MultiChapterTypes { get; set; }
        #endregion

        #region دسترسی ها

        public virtual DbSet<AccessType> AccessTypes { get; set; }
        public virtual DbSet<AccessList> AccessLists { get; set; }

        #endregion

        #region Flash Memory

        public virtual DbSet<HddMemory> HddMemories { get; set; }
        public virtual DbSet<HddMemoryPerson> HddMemoryPersons { get; set; }
        public virtual DbSet<CopiedFilmToMemory> CopiedFilmTos { get; set; }

        #endregion


        #region Link Web Site

        public virtual DbSet<LinkHeader> LinkHeaders { get; set; }
        public virtual DbSet<LinkDetail> LinkDetails { get; set; }

        #endregion

        #region Chanel Bot
        public virtual DbSet<BFactorHeader> FactorHeaders { get; set; }
        public virtual DbSet<BFactorDetail> FactorDetails { get; set; }
        public virtual DbSet<ChanelMember> ChanelMembers { get; set; }

        #endregion

        //#region Factor 
        ////Financial
        //public virtual DbSet<FactorHeader> FactorHeaders { get; set; }
        //public virtual DbSet<FactorDetails> FactorHeaders { get; set; }

        //#endregion

        #endregion
        public virtual DbSet<ErrorExcep> ErrorExceps { get; set; }
    }
}