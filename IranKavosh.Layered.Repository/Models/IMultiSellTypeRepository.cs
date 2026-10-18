using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiSellTypeRepository : IGenericRepository<MultiSellType>
    {
       
    }

    public class MultiSellTypeRepository : GenericRepository<MultiSellType>, IMultiSellTypeRepository
    {
        public MultiSellTypeRepository(dbCore context) : base(context) { }
    }
}
