using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiListPersonal", Schema = "Movie")]
    public class MultiListPersonal : ModelId
    {
        public Guid PersonalId { get; set; }
        public virtual Personal Personal { get; set; }

        public Guid ChapterDetailId { get; set; }
        public virtual MultimediaChapterDetail ChapterDetail { get; set; }

        /// <summary>
        /// نوع فروش:
        /// موبایل
        /// سیستم
        /// </summary>
        public Guid SellTypeId { get; set; }
        public virtual MultiSellType SellType { get; set; }
        
        public long Price { get; set; }     
        /// <summary>
        /// تسویه
        /// </summary>
        public bool Clearing { get; set; }

        public MultiListPersonal()
        {
            Clearing = false;
        }

    }
}   