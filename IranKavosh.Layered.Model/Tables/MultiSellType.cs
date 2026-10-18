using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiSellType", Schema = "Movie")]
    public class MultiSellType : ModelId
    {
        public string Title { get; set; }

        [ForeignKey("SellTypeId")]
        public virtual ICollection<MultiListPersonal> MultiListPersonals { get; set; }
    }
}