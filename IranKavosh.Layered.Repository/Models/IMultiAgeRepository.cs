using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiAgeRepository : IGenericRepository<MultiAge>
    {
       
    }

    public class MultiAgeRepository : GenericRepository<MultiAge>, IMultiAgeRepository
    {
        public MultiAgeRepository(dbCore context) : base(context) { }
    }
}
