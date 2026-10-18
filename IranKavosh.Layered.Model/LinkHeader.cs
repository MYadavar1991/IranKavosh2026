using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;

namespace IranKavosh.Layered.Model
{
    [Table(name: "LinkHeader", Schema = "Uri")]
    public class LinkHeader : ModelId
    {
        public long Code { get; set; }
        public string Title { get; set; }

        [ForeignKey("LinkHeaderId")]
        public virtual ICollection<LinkDetail> LinkDetails { get; set; }
    }
}       