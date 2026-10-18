using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IBuySellDetailRepository : IGenericRepository<BuySellDetail>
    {
       
    }

    public class BuySellDetailRepository : GenericRepository<BuySellDetail>, IBuySellDetailRepository
    {
        public BuySellDetailRepository(dbCore context) : base(context) { }
    }
}
