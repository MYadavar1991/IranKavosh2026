using System.Net.Http.Json;
using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class ErrorExcepHttpService : HttpServiceBase, IReadService<ErrorExcep>, IWriteService<ErrorExcep>
    {
        public ErrorExcepHttpService(HttpClient client) : base(client)
        {
        }
        public async Task<IEnumerable<ErrorExcep>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<ErrorExcep>>(
                url: "ErrorExcep/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<ErrorExcep>>("ErrorExcep");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var ErrorExceps = await GetAsync();
            //return ErrorExceps.Any();
            return false;
        }

        public async Task<ErrorExcep> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<ErrorExcep>(
                url: $"ErrorExcep/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(ErrorExcep entity)
        {
            await SendRequestAsync<object>(
                url: "ErrorExcep/AddOrUpdate",
                //url: "ErrorExcep",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(ErrorExcep entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"ErrorExcep/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"ErrorExcep/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
