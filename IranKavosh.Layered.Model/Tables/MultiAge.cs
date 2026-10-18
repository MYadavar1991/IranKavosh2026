using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiAge", Schema = "Movie")]
    public class MultiAge : ModelId
    {
        public string Title { get; set; }

        [ForeignKey("AgeId")]
        public virtual ICollection<MultimediaHeader> MultimediaHeaders { get; set; }
    }
}