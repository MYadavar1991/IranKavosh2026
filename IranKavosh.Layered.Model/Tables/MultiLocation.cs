using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{
    [Table(name: "MultiLocation", Schema = "Movie")]
    public class MultiLocation : ModelId
    {
        /// <summary>
        /// آدرس دقیق پوشه ریشه در سیستم (مثال: D:\Movies)
        /// </summary>
        public string Root { get; set; }
        /// <summary>
        /// آدرس کامل پوشه Share - نام سیستم مثل: \\PC-12345\Video4
        /// </summary>
        public string DirShare_PCName { get; set; }
        /// <summary>
        /// آدرس کامل پوشه Share - آی پی سیستم مثل: \\192.168.1.160\Video4
        /// </summary>
        public string DirShare_PCIP { get; set; }
        public string UserWin { get; set; }
        public string PassWin { get; set; }
        public string? WebLink { get; set; } 

        [ForeignKey("LocationId")]
        public virtual ICollection<MultimediaHeader> MultimediaHeaders { get; set; }

    }
}