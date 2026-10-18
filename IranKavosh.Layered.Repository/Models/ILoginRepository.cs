using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface ILoginRepository : IGenericRepository<Login>
    {
       
    }

    public class LoginRepository : GenericRepository<Login>, ILoginRepository
    {
        public LoginRepository(dbCore context) : base(context) { }
    }
}
