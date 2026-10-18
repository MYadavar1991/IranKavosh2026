using IranKavosh.Layered.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IranKavosh.Layered.Service.Interface
{
    namespace IranKavosh.Layered.Service.Interfaces
    {
        public interface IReadService<T> where T : class
        {
           Task<bool> Any(Guid id);
           Task<T> GetById(Guid id);
           Task<IEnumerable<T>> GetAsync();

        }   

        public interface IWriteService<in T> where T : class
        {   
           Task AddAsync(T entity);     
           Task UpdateAsync(T entity);
          
           Task DeleteAsync(Guid id);
        }
        public interface IWriteServiceActor
        {
            Task AddIfNewAsync(List<string> entity);
            
        }
        public interface IWriteServiceFund<in T> where T : class
        {
            Task UpdateRangeDateAsync(DateTime date);

        }
    }
}

