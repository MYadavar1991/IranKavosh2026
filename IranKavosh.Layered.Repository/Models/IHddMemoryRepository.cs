using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IHddMemoryRepository : IGenericRepository<HddMemory>
    {
       
    }

    public class HddMemoryRepository : GenericRepository<HddMemory>, IHddMemoryRepository
    {
        public HddMemoryRepository(dbCore context) : base(context) { }
    }
}
