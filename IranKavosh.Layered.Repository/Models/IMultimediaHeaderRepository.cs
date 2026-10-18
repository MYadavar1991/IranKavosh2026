using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultimediaHeaderRepository : IGenericRepository<MultimediaHeader>
    {
       
    }

    public class MultimediaHeaderRepository : GenericRepository<MultimediaHeader>, IMultimediaHeaderRepository
    {
        public MultimediaHeaderRepository(dbCore context) : base(context)
        {
            context.MultimediaHeaderChapters.ToList();
            context.MultimediaChapterDetails.ToList();
            context.Prices.ToList();
        }
    }
}
