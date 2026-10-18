using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class FundTypeHttpService : HttpServiceBase, IReadService<FundType>, IWriteService<FundType>
    {
        public FundTypeHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<FundType>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<FundType>>(
                url: "FundType/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<FundType> GetById(Guid id)
        {
            return await SendRequestAsync<FundType>(
                url: $"FundType/GetById/{id}",
                method: HttpMethod.Get
            );
        }
        // خطا دارد
        //public async Task<FundType> GetByTitle(string data)
        //{
        //    var getSerial = ClassEncryptDecrypt.Encrypt(data);
        //    return await SendRequestAsync<FundType>(
        //        url: $"FundType/GetByTitle/{getSerial}",
        //        method: HttpMethod.Get
        
        //    );
        //}
        public async Task AddAsync(FundType entity)
        {
            await SendRequestAsync<object>(
                url: "FundType/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }
   
        public async Task UpdateAsync(FundType entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"FundType/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"FundType/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
