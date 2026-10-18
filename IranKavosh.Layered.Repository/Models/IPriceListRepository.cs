using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IPriceListRepository : IGenericRepository<PriceList>
    {
       
    }

    public class PriceListRepository : GenericRepository<PriceList>, IPriceListRepository
    {
        public PriceListRepository(dbCore context) : base(context) { }
    }
}
