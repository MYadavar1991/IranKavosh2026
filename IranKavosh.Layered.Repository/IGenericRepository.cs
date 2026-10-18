using IranKavosh.Layered.Model.Public;
using System.Collections;
using System.Linq.Expressions;

namespace IranKavosh.Layered.Repository
{
    public interface IGenericRepository<T> where T : ModelId, new()
    {
        Task<bool> Any();
        Task<bool> Any(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task<T> GetById(Guid id);   

        //Task<IEnumerable<T>> GetAllModel(Expression<Func<T, T>> select);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<T> FindFirst(Expression<Func<T, bool>> expression);
        //Task<T> FirstOrNew(Expression<Func<T, bool>> expression);
            
        Task Update(T entity);  
        Task UpdateRange(List<T> entity);  
        void Add(T entity);
        Task AddOrUpdate(T entity);

        void AddRange(IEnumerable<T> entities); 
        void Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);

        Task<(T item, bool gNew)> GetOrNew(Guid id);  
        Task<(T item, bool gNew)> GetOrNew(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> OrderBy(Expression<Func<T, object>> expression);
        Task<IEnumerable<T>> OrderByDesc(Expression<Func<T, object>> expression);

        long MaxCodePerson(bool plus);
    }
}
      