using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;
using IranKavosh.Layered.Model.Tables.BOT;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "Product", Schema = "Product")]
    public class Product : ModelId
    {
        [DisplayName("بارکد")]
        public long BarCode { get; set; }

        [DisplayName("کد")]
        public long Code { get; set; }

        [DisplayName("نام کالا در سیستم")]
        public string NameApp { get; set; }

        [DisplayName("نام کالا در چاپ")]
        public string? NamePrint { get; set; }

        [DisplayName("مدل")]
        public string? Model { get; set; }

        /// <summary>
        /// فروش فیلم - خدمات چاپ - لوازم جانبی - خدمات اینترنتی - خدمات رایانه
        /// </summary>
        public Guid? FundTypeId { get; set; }
        public virtual FundType FundType { get; set; }

        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public string? Company { get; set; }
        public string? Description { get; set; }
        [DisplayName("محاسبه موجودی")]
        public bool Inventory { get; set; }


        // public virtual ICollection<ProductListGroup> ProductListGroups { get; set; }
    

        [ForeignKey("ProductId")]
        public virtual ICollection<PriceList> PriceLists { get; set; }
        [ForeignKey("ProductId")]
        public virtual ICollection<BFactorDetail> BFactorDetails { get; set; }
        // public virtual IList<BuySellHistory> BuySellHistories { get; set; }


    }



}