using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface ICopiedFilmToMemoryRepository : IGenericRepository<CopiedFilmToMemory>
    {
       
    }

    public class CopiedFilmToMemoryRepository : GenericRepository<CopiedFilmToMemory>, ICopiedFilmToMemoryRepository
    {
        public CopiedFilmToMemoryRepository(dbCore context) : base(context) { }
    }
}
