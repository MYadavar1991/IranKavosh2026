using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "BuySellDetail", Schema = "Financial")]
    public class BuySellDetail : ModelId
    {
        //public Guid BuySellHeaderId { get; set; }
        //public virtual BuySellDetail BuySellHeader { get; set; }
        public long Code { get; set; }
        public Guid GroupId { get; set; }

        public Guid PriceListId { get; set; }
        public virtual PriceList PriceList { get; set; }    

        public float Count { get; set; }

        //public bool Calculated { get; set; }
        //public DateTime? CalculateDateTime { get; set; }

        /// <summary>
        /// فروش
        /// </summary>
        public long PriceSell { get; set; }

        /// <summary>
        ///  خرید   
        /// </summary>
        public long PriceBuy { get; set; }

        /// <summary>
        /// قیمت ایران کاوش
        /// --
        /// پرداختی مشتری
        /// </summary>
        public long PriceFinal { get; set; }

        /// <summary>
        /// Sell = True = فروش
        /// </summary>
        public bool Sell { get; set; }

        public BuySellDetail()
        {
            PriceSell = 0;
            PriceBuy = 0;
            PriceFinal = 0;
        }
    }

}