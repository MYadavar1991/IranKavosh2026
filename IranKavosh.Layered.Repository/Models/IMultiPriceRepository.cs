using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiPriceRepository : IGenericRepository<MultiPrice>
    {
       
    }

    public class MultiPriceRepository : GenericRepository<MultiPrice>, IMultiPriceRepository
    {
        public MultiPriceRepository(dbCore context) : base(context) { }
    }
}
