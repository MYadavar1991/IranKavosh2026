using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultimediaChapterDetailRepository : IGenericRepository<MultimediaChapterDetail>
    {
       
    }

    public class MultimediaChapterDetailRepository : GenericRepository<MultimediaChapterDetail>, IMultimediaChapterDetailRepository
    {
        public MultimediaChapterDetailRepository(dbCore context) : base(context) { }
    }
}
