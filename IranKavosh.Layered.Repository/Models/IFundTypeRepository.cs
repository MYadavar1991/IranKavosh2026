using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IFundTypeRepository : IGenericRepository<FundType>
    {
       
    }

    public class FundTypeRepository : GenericRepository<FundType>, IFundTypeRepository
    {
        public FundTypeRepository(dbCore context) : base(context) { }
    }
}
