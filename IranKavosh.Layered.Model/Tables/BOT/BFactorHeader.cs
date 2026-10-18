using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Model.Tables.BOT
{
    [Table(name: "BFactorHeader", Schema = "Bot")]
    public class BFactorHeader : ModelId
    {
        public long Code { get; set; }
        public DateTime Date { get; set; }
        public string Header { get; set; }
        public Guid BuyerId { get; set; }
        public Personal Buyer { get; set; }




        [ForeignKey("FactorHeaderId")]
        public virtual ICollection<BFactorDetail> BFactorDetail { get; set; }
    }
}