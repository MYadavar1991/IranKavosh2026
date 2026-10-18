using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultimediaHeaderChapter", Schema = "Movie")]
    public class MultimediaHeaderChapter : ModelId
    {
        public Guid MultimediaHeaderId { get; set; }
        public virtual MultimediaHeader MultimediaHeader { get; set; }

        public int Code { get; set; }
        /// <summary>
        /// معرفی فصل
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// سال ساخت
        /// </summary>
        public string Created { get; set; }

        public string? Description { get; set; }

        [ForeignKey("HeaderChapterId")]
        public virtual ICollection<MultimediaChapterDetail> MultimediaChapterDetails { get; set; }
    }
}