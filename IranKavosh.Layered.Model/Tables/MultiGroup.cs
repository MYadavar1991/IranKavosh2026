using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiGroup", Schema = "Movie")]
    public class MultiGroup : ModelId
    {
        public string Title { get; set; }

        public virtual ICollection<MultimediaHeader> MultimediaHeaders { get; set; }
    }
}