using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IAccessTypeRepository : IGenericRepository<AccessType>
    {
       
    }

    public class AccessTypeRepository : GenericRepository<AccessType>, IAccessTypeRepository
    {
        public AccessTypeRepository(dbCore context) : base(context) { }
    }
}
