using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiCountry", Schema = "Movie")]
    public class MultiCountry : ModelId
    {
        public string Title { get; set; }

        [ForeignKey("CountryId")]
        public virtual ICollection<MultimediaHeader> MultimediaHeaders { get; set; }
    }
}