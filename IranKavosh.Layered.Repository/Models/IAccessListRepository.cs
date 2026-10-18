using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IAccessListRepository : IGenericRepository<AccessList>
    {
       
    }

    public class AccessListRepository : GenericRepository<AccessList>, IAccessListRepository
    {
        public AccessListRepository(dbCore context) : base(context) { }
    }
}
