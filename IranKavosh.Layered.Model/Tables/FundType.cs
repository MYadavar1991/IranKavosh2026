using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "FundType", Schema = "Financial")]
    public class FundType : ModelId
    {
        public string Title { get; set; }

        [ForeignKey("FundTypeId")]
        public virtual ICollection<Product> Products { get; set; }

        //[ForeignKey("FundTypeId")]
        //public virtual ICollection<SalesService> SalesServices { get; set; }
    }
}