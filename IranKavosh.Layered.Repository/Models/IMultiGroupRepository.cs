using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiGroupRepository : IGenericRepository<MultiGroup>
    {
       
    }

    public class MultiGroupRepository : GenericRepository<MultiGroup>, IMultiGroupRepository
    {
        public MultiGroupRepository(dbCore context) : base(context) { }
    }
}
