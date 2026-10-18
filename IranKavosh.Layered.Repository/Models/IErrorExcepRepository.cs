using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.Repository.Models
{
    public interface IErrorExcepRepository : IGenericRepository<ErrorExcep>
    {
       
    }

    public class ErrorExcepRepository : GenericRepository<ErrorExcep>, IErrorExcepRepository
    {
        public ErrorExcepRepository(dbCore context) : base(context) { }
    }
}
