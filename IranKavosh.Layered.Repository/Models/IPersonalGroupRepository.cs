using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IPersonalGroupRepository : IGenericRepository<PersonalGroup>
    {
       
    }

    public class PersonalGroupRepository : GenericRepository<PersonalGroup>, IPersonalGroupRepository
    {
        public PersonalGroupRepository(dbCore context) : base(context) { }
    }
}
