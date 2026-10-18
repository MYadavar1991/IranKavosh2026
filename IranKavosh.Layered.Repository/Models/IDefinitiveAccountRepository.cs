using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IDefinitiveAccountRepository : IGenericRepository<DefinitiveAccount>
    {
       
    }

    public class DefinitiveAccountRepository : GenericRepository<DefinitiveAccount>, IDefinitiveAccountRepository
    {
        public DefinitiveAccountRepository(dbCore context) : base(context) { }
    }
}
