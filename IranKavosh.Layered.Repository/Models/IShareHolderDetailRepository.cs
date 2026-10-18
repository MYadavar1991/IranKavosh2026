using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IShareHolderDetailRepository : IGenericRepository<ShareHolderDetail>
    {
       
    }

    public class ShareHolderDetailRepository : GenericRepository<ShareHolderDetail>, IShareHolderDetailRepository
    {
        public ShareHolderDetailRepository(dbCore context) : base(context) { }
    }
}
