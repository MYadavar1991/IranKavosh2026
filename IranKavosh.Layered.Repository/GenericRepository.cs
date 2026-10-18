using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Public;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IranKavosh.Layered.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelId, new()
    {
        protected readonly dbCore context;
        public GenericRepository(dbCore context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public Task AddOrUpdate(T entity)
        {
            if (context.Set<T>().Any(a => a.Id == entity.Id))
                context.Set<T>().Update(entity);
            else
                context.Set<T>().Add(entity);
            return Task.CompletedTask;
            // Notting Core is here For Used UnitOfWork  Pattern Engine Febofer !!!

        }

        public async Task<T> FindFirst(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().First(expression);
        }

        //public async Task<T> FirstOrNew(Expression<Func<T, bool>> expression)
        //{
        //    if (context.Set<T>().Any(expression))
        //    {
        //        return await context.Set<T>().FirstAsync(expression);
        //    }

        //    return new T();
        //}

        public Task Update(T entity)
        {
            context.Set<T>().Update(entity);

            //context.Set<T>().Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;


            return Task.CompletedTask;

        }
        public Task UpdateRange(List<T> entity)
        {
            context.Set<T>().UpdateRange(entity);

            //context.Set<T>().Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;


            return Task.CompletedTask;

        }
        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }
        public Task<IEnumerable<T>> GetAll()
        {
            return Task.FromResult(context.Set<T>().Where(w => !w.IsDelete).AsEnumerable());
        }

        //public Task<IEnumerable<T>> GetAllModel(Expression<Func<T, T>> select)
        //{
        //    //Expression<Func<T, T>> expression = s => new T { Id = s.Id, IsDelete = s.IsDelete , DateTime = s.DateTime};
        //    var get = Task.FromResult(context.Set<T>().Where(w => !w.IsDelete).Select(select).AsEnumerable());
        //    return get;
        //}

        public Task<bool> Any()
        {
            return Task.FromResult(context.Set<T>().Any());
        }
        public Task<bool> Any(Guid id)
        {
            return Task.FromResult(context.Set<T>().Any(a => a.Id == id));
        }
        public Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(context.Set<T>().Any(expression));
        }

        public Task<T> GetById(Guid id)
        {
            return Task.FromResult(context.Set<T>().Find(id));
        }
        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public async Task RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }

        public async Task<(T item, bool gNew)> GetOrNew(Guid id)
        {
            if (!context.Set<T>().Any(a => a.Id == id))
                return new ValueTuple<T, bool>(new T(), true);

            return new ValueTuple<T, bool>(context.Set<T>().First(f => f.Id == id), true);
        }

        public async Task<(T item, bool gNew)> GetOrNew(Expression<Func<T, bool>> expression)
        {
            if (!context.Set<T>().Any(expression))
                return new ValueTuple<T, bool>(new T(), true);

            return new ValueTuple<T, bool>(context.Set<T>().First(expression), false);
        }

        public Task<IEnumerable<T>> OrderBy(Expression<Func<T, object>> expression)
        {
            return Task.FromResult(context.Set<T>().OrderBy(expression).AsEnumerable());
        }
        public Task<IEnumerable<T>> OrderByDesc(Expression<Func<T, object>> expression)
        {
            return Task.FromResult(context.Set<T>().OrderByDescending(expression).AsEnumerable());
        }

        public long MaxCodePerson(bool plus)
        {
            var getMax = context.Personals.Select(s => s.Code).DefaultIfEmpty(0).Max();
            if (plus)
                getMax++;

            return getMax;
        }
    }
}