using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "PriceListDiscount", Schema = "Financial")]
  
    public class PriceListDiscount : ModelId
    {
        public long Code { get; set; }
        public Guid PriceListId { get; set; }   
        public virtual PriceList PriceList { get; set; }

        public int Count { get; set; } // تعداد برای تخفیف
        public float Discount { get; set; } // درصد تخفیف 
        public string Description { get; set; }

        public PriceListDiscount()
        {
            Description = "";
        }
    }
}