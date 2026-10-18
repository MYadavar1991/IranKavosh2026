using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Table_Bot;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IChanelMemberRepository : IGenericRepository<ChanelMember>
    {
       
    }

    public class ChanelMemberRepository : GenericRepository<ChanelMember>, IChanelMemberRepository
    {
        public ChanelMemberRepository(dbCore context) : base(context) { }
    }
}
