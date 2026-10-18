using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiChapterType", Schema = "Movie")]
    public class MultiChapterType : ModelId
    {
        public string Title { get; set; }

        public virtual ICollection<MultimediaChapterDetail> ChapterDetails { get; set; }
    }   
}