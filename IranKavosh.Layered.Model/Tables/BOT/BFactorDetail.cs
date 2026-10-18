using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Model.Tables.BOT
{
    [Table(name: "BFactorDetail", Schema = "Bot")]
    public class BFactorDetail : ModelId
    {
        public Guid FactorHeaderId { get; set; }
        public BFactorHeader FactorHeader { get; set; }

        [Display(Name = "شماره")]
        public long Code { get; set; }
        [Display(Name = "محصول|خدمات")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Display(Name = "تعداد")]
        public float Count { get; set; }

        [Display(Name = "قیمت خرید")]
        public long PriceSell { get; set; }
        [Display(Name = "قیمت مشتری")]
        public long PriceUnit { get; set; }
    }
}    
    
    
