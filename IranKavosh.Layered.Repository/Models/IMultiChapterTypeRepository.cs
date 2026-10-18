using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiChapterTypeRepository : IGenericRepository<MultiChapterType>
    {
       
    }

    public class MultiChapterTypeRepository : GenericRepository<MultiChapterType>, IMultiChapterTypeRepository
    {
        public MultiChapterTypeRepository(dbCore context) : base(context) { }
    }
}
