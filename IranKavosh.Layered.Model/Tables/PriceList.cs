using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "PriceList", Schema = "Financial")]
    public class PriceList : ModelId
    {
        public long Code { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public long Price { get; set; } // مبلغ بدون تخفیف
        public string Title  { get; set; }

        /// <summary>
        /// ثابت در لیست
        /// </summary>
        public bool Const  { get; set; }

        [ForeignKey("PriceListId")]
        public virtual ICollection<PriceListDiscount> PriceListDiscounts { get; set; }
        [ForeignKey("PriceListId")]
        public virtual ICollection<BuySellDetail> BuySellDetails { get; set; }
        public PriceList()
        {
            Title = "";
        }
    }
}