using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiGenreRepository : IGenericRepository<MultiGenre>
    {
       
    }

    public class MultiGenreRepository : GenericRepository<MultiGenre>, IMultiGenreRepository
    {
        public MultiGenreRepository(dbCore context) : base(context) { }
    }
}
