using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "Genre", Schema = "Movie")]
    public class MultiGenre : ModelId
    {
        public string Title { get; set; }
        public string TitleEn { get; set; }
      //  public virtual ICollection<MultiGenreList> MultiGenreLists { get; set; }
    }
}