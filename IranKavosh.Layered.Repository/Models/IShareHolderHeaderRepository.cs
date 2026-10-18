using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IShareHolderHeaderRepository : IGenericRepository<ShareHolderHeader>
    {
       
    }

    public class ShareHolderHeaderRepository : GenericRepository<ShareHolderHeader>, IShareHolderHeaderRepository
    {
        public ShareHolderHeaderRepository(dbCore context) : base(context) { }
    }
}
