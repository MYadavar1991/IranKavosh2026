using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Tables
{

    [Table(name: "ShareHolderHeader", Schema = "Stocks")]
    public class ShareHolderHeader : ModelId
    {
        public long Code { get; set; }
        public string Title { get; set; }

        [ForeignKey("ShareHolderHeaderId")]
        public virtual ICollection<ShareHolderDetail> ShareHolderDetails { get; set; }
    }
}