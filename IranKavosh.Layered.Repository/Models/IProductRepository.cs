using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IProductRepository : IGenericRepository<Product>
    {
       
    }

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(dbCore context) : base(context) { }
    }
}
