using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultimediaHeader", Schema = "Movie")]
    public class MultimediaHeader : ModelId
    {
        /// <summary>
        /// شماره
        /// </summary>
        public long Num { get; set; }
        public float IMDB { get; set; }

        /// <summary>
        /// نام فارسی فیلم
        /// </summary>
        public string NamePersian { get; set; }
        /// <summary>
        /// نام انگلیسی فیلم
        /// </summary>
        public string NameEnglish { get; set; }

        /// <summary>
        /// سال ساخت
        /// </summary>  
        public string Created { get; set; }
        public string? Description { get; set; }

        public bool IsAnimated { get; set; }
        public bool IsAnime { get; set; }
        public Guid AgeId { get; set; }
        public virtual MultiAge Age { get; set; }
        
        public int? DayOfWeek { get; set; }

        public Guid PriceId { get; set; }
        public virtual MultiPrice Price { get; set; }     

        public string? ActorsId { get; set; }      
       

        /// <summary>
        /// ایرانی - آمریکایی - هندی - ترکی - ژاپن - چینی - کره - سایر
        /// </summary>
        public Guid CountryId { get; set; }
        public virtual MultiCountry Country { get; set; }

        /// <summary>
        /// سریال - سینمایی
        /// </summary>
        public Guid MultimediaGroupId { get; set; }
        public virtual MultiGroup MultimediaGroup { get; set; }
      
        public Guid? LocationId { get; set; }
        public virtual MultiLocation Location { get; set; }

        public string MultiGenreLists { get; set; }

        // public virtual ICollection<MultiGenreList> MultiGenreLists { get; set; }
        public virtual ICollection<MultimediaHeaderChapter> HeaderChapters { get; set; }

        public MultimediaHeader()
        {
            
        }

    }
}