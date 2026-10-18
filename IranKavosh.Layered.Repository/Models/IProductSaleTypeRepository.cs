using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IProductSaleTypeRepository : IGenericRepository<SalesService>
    {
       
    }

    public class ProductSaleTypeRepository : GenericRepository<SalesService>, IProductSaleTypeRepository
    {
        public ProductSaleTypeRepository(dbCore context) : base(context) { }
    }
}
