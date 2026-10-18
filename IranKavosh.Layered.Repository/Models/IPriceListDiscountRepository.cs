using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IPriceListDiscountRepository : IGenericRepository<PriceListDiscount>
    {
       
    }

    public class PriceListDiscountRepository : GenericRepository<PriceListDiscount>, IPriceListDiscountRepository
    {
        public PriceListDiscountRepository(dbCore context) : base(context) { }
    }
}
