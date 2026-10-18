using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface ISellDetailRepository : IGenericRepository<BuySellDetail>
    {
       
    }

    public class SellDetailRepository : GenericRepository<BuySellDetail>, ISellDetailRepository
    {
        public SellDetailRepository(dbCore context) : base(context) { }
    }
}
