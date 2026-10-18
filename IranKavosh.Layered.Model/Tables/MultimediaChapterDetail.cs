using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultimediaChapterDetail", Schema = "Movie")]
    public class MultimediaChapterDetail : ModelId
    {
        //public Guid MultimediaHeaderId { get; set; }
        //public virtual MultimediaHeader MultimediaHeader { get; set; }

        public Guid HeaderChapterId { get; set; }
        public virtual MultimediaHeaderChapter HeaderChapter { get; set; }

        public Guid? ChapterTypeId { get; set; }
        public virtual MultiChapterType ChapterType { get; set; }

        public bool IsWebDl { get; set; }
        //public bool IsSubtitle { get; set; }

        public long Code { get; set; }  
        /// <summary>
        /// کیفیت پرده
        /// </summary>
        public bool IsHDTS { get; set; }  

        //public bool Persian { get; set; } // دوبله
        //public bool HardSub { get; set; }  // زیرنویس

        /// <summary>
        /// پسوند
        /// </summary>  
        public string? Extension { get; set; }
         
        
        public string? Description { get; set; }

        [ForeignKey("ChapterDetailId")]
        public virtual ICollection<MultiListPersonal> MultiListPersonals { get; set; }
        [ForeignKey("ChapterDetailId")]
        public virtual ICollection<CopiedFilmToMemory> CopiedFilmToMemories { get; set; }

        public MultimediaChapterDetail()
        {
            //Persian = false;
            //HardSub = false;
        }
    }
}