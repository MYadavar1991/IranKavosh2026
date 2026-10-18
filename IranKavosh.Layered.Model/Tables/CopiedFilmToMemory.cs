using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "CopiedFilmToMemory", Schema = "Save")]
    public class CopiedFilmToMemory : ModelId
    {
        public Guid GroupId { get; set; }   
        public Guid HddMemoryId { get; set; }
        public HddMemory HddMemory { get; set; }

        public Guid ChapterDetailId { get; set; }
        public MultimediaChapterDetail ChapterDetail { get; set; }

        public long Price { get; set; }
        public bool Calc { get; set; }
    
    }
}