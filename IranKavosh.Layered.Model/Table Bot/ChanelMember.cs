using System.ComponentModel.DataAnnotations.Schema;
using IranKavosh.Layered.Model.Public;

namespace IranKavosh.Layered.Model.Table_Bot
{
    [Table(name: "ChanelMember", Schema = "Bot")]

    public class ChanelMember : ModelId
    {
        public long ReferralSend { get; set; }
        public long ReferralReceive { get; set; }
        public DateTime MemberDate { get; set; }
    }
}           