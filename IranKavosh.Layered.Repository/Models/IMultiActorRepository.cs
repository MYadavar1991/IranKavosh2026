using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IMultiActorRepository : IGenericRepository<MultiActor>
    {
       
    }

    public class MultiActorRepository : GenericRepository<MultiActor>, IMultiActorRepository
    {
        public MultiActorRepository(dbCore context) : base(context) { }
    }
}
