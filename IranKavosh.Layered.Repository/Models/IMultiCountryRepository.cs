using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiCountryRepository : IGenericRepository<MultiCountry>
    {
       
    }

    public class MultiCountryRepository : GenericRepository<MultiCountry>, IMultiCountryRepository
    {
        public MultiCountryRepository(dbCore context) : base(context) { }
    }
}
