using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace IranKavosh.Layered.Model.Tables
{
   
    [Table(name: "Fund", Schema = "Financial")]
    public class Fund : ModelId
    {
        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public long Code { get; set; }
        /// <summary>
        /// 1 = کارت خان
        /// 2 = نقدی
        /// 3 = بدهی
        /// </summary>
        public int? SellType { get; set; }

        ///// <summary>
        ///// فیلم - خدمات چاپ - لوازم جانبی - خدمات اینترنتی
        ///// </summary>
        //public Guid? FundTypeId { get; set; }
        //public virtual FundType FundType { get; set; }

        /// <summary>
        /// جمع قسمت های فروش
        /// مثال: GroupId Memory
        /// مثال: ثبت فاکتور فروش پرینت
        /// </summary>
        public Guid? GlobalId { get; set; }

        public long Count { get; set; }
        [Display(Name = "مبلغ کل")]
        public long PriceTotal { get; set; }

        [Display(Name = "دریافتی")]
        public long PriceReceived { get; set; }

    
        public bool Calculation { get; set; }
        public string TableName { get; set; }

        public Guid? RegisterId { get; set; }
        public Personal Register { get; set; }
    }
}               