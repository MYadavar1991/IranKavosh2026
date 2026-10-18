using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiGenreListRepository : IGenericRepository<MultiGenreList>
    {
       
    }

    public class MultiGenreListRepository : GenericRepository<MultiGenreList>, IMultiGenreListRepository
    {
        public MultiGenreListRepository(dbCore context) : base(context) { }
    }
}
