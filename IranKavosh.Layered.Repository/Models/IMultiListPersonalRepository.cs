using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiListPersonalRepository : IGenericRepository<MultiListPersonal>
    {
       
    }

    public class MultiListPersonalRepository : GenericRepository<MultiListPersonal>, IMultiListPersonalRepository
    {
        public MultiListPersonalRepository(dbCore context) : base(context) { }
    }
}
