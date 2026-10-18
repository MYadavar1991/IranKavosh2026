using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IPersonalRepository : IGenericRepository<Personal>
    {
       
    }

    public class PersonalRepository : GenericRepository<Personal>, IPersonalRepository
    {
        public PersonalRepository(dbCore context) : base(context) { }
    }
}
