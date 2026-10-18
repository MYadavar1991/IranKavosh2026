using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Model.Tables.BOT;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IBFactorHeaderRepository : IGenericRepository<BFactorHeader>
    {
       
    }

    public class BFactorHeaderRepository : GenericRepository<BFactorHeader>, IBFactorHeaderRepository
    {
        public BFactorHeaderRepository(dbCore context) : base(context) { }
    }
}
