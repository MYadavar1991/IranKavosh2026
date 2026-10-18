using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IFundRepository : IGenericRepository<Fund>
    {
       
    }

    public class FundRepository : GenericRepository<Fund>, IFundRepository
    {
        public FundRepository(dbCore context) : base(context) { }
    }
}
