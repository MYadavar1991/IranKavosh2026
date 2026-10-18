namespace IranKavosh.Layered.Service.Middleware
{
    public class ModelChapterDetailsFull
    {
        public Guid ChapterDetailsId { get; set; }
        public Guid HeaderId { get; set; }
        public Guid HeaderChapterId { get; set; }

        /// <summary>
        /// Fa-1  |  En-1
        /// </summary>
        public string Type { get; set; }    
        public string HeaderName { get; set; }
        public string AddressFile { get; set; }
        public long HeaderCode { get; set; }
        public long ChapterCode { get; set; }
        public int HeaderChapterCode { get; set; }
        public string ImageDir { get; set; }


    }   

   public class ModelGuidFaEn
    {
        public Guid ChapterDetailId { get; set; }
        /// <summary>
        /// Fa-1  |  En-1
        /// </summary>
        public string Type { get; set; }
    }
}
