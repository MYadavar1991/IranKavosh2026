using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Service.Middleware
{
    public class ModelMultiHeader : ModelId
    {

        /// <summary>
        /// شماره
        /// </summary>
        public long Num { get; set; }

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

        public bool IsAnimated { get; set; }
        public bool IsAnime { get; set; }
        public Guid AgeId { get; set; }


        public int? DayOfWeek { get; set; }

        public Guid PriceId { get; set; }

        public string? ActorsId { get; set; }

        /// <summary>
        /// ایرانی - آمریکایی - هندی - ترکی - ژاپن - چینی - کره - سایر
        /// </summary>
        public Guid CountryId { get; set; }

        /// <summary>
        /// سریال - سینمایی
        /// </summary>
        public Guid MultimediaGroupId { get; set; }
        public Guid? LocationId { get; set; }
        //public float IMDB { get; set; }
        public string MultiGenreLists { get; set; }

        
    }
    public class ModelMultiHeaderPoster : ModelId
    {

        /// <summary>   
        /// کد فیلم
        /// </summary>
        public long Num { get; set; }

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

        public bool IsAnimated { get; set; }
        public bool IsAnime { get; set; }
            
        public string? Actors { get; set; }

        /// <summary>
        /// ایرانی - آمریکایی - هندی - ترکی - ژاپن - چینی - کره - سایر
        /// </summary>
        public string Country { get; set; }
        public string Poster { get; set; }

        /// <summary>
        /// سریال - سینمایی
        /// </summary>
        public string MultimediaGroup { get; set; } 
        public float IMDB { get; set; }
        public string Description { get; set; }
        public string MultiGenreLists { get; set; }


    }
}
