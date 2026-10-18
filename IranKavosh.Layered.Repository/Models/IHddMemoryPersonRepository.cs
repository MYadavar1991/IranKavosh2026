using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IHddMemoryPersonRepository : IGenericRepository<HddMemoryPerson>
    {
       
    }

    public class HddMemoryPersonRepository : GenericRepository<HddMemoryPerson>, IHddMemoryPersonRepository
    {
        public HddMemoryPersonRepository(dbCore context) : base(context) { }
    }
}
