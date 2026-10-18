using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiLocationRepository : IGenericRepository<MultiLocation>
    {
       
    }

    public class MultiLocationRepository : GenericRepository<MultiLocation>, IMultiLocationRepository
    {
        public MultiLocationRepository(dbCore context) : base(context) { }
    }
}
