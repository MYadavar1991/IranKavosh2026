using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultimediaHeaderChapterRepository : IGenericRepository<MultimediaHeaderChapter>
    {
       
    }

    public class MultimediaHeaderChapterRepository : GenericRepository<MultimediaHeaderChapter>, IMultimediaHeaderChapterRepository
    {
        public MultimediaHeaderChapterRepository(dbCore context) : base(context)
        {
            //context.MultimediaChapterDetails.ToList();
            //context.MultimediaHeaders.ToList();
        }
    }
}
