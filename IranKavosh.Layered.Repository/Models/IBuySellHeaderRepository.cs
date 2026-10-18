using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IBuySellHeaderRepository : IGenericRepository<BuySellDetail>
    {
       
    }

    public class BuySellHeaderRepository : GenericRepository<BuySellDetail>, IBuySellHeaderRepository
    {
        public BuySellHeaderRepository(dbCore context) : base(context) { }
    }
}
