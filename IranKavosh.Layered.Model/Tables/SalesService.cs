using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "SalesService", Schema = "Product")]
    public class SalesService : ModelId
    {
        [DisplayName("خدمات فروش")]
        public string Title { get; set; }
        /// <summary>
        /// جهت دسته بندی و فیلترینگ
        /// فیلم - خدمات چاپ - لوازم جانبی - خدمات اینترنتی
        /// </summary>
        public Guid FundTypeId { get; set; }
        public virtual FundType FundType { get; set; }
        public long Price { get; set; }

        //public virtual ICollection<Product> Products { get; set; }
    }
}