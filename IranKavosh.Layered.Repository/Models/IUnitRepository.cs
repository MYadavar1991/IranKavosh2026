using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IUnitRepository : IGenericRepository<Unit>
    {
       
    }

    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(dbCore context) : base(context) { }
    }
}
