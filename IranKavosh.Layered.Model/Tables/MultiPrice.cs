using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiPrice", Schema = "Movie")]
    public class MultiPrice : ModelId
    {
        public int Price { get; set; }

        [ForeignKey("PriceId")]
        public virtual ICollection<MultimediaHeader> MultimediaHeaders { get; set; }
    }
}