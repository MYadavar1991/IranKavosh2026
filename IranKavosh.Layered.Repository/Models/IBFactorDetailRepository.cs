using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Model.Tables.BOT;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IBFactorDetailRepository : IGenericRepository<BFactorDetail>
    {
       
    }

    public class BFactorDetailRepository : GenericRepository<BFactorDetail>, IBFactorDetailRepository
    {
        public BFactorDetailRepository(dbCore context) : base(context) { }
    }
}
