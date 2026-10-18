using IranKavosh.Layered.Model.Public;
using System.ComponentModel.DataAnnotations.Schema;

namespace IranKavosh.Layered.Model
{
    [Table(name: "LinkDetail", Schema = "Uri")]

    public class LinkDetail : ModelId
    {
        public long Code { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public Guid LinkHeaderId { get; set; }
        public virtual LinkHeader LinkHeader { get; set; }

    }
}