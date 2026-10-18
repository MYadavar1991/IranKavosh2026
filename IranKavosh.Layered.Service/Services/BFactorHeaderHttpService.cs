using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Model.Tables.BOT;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class BFactorHeaderHttpService : HttpServiceBase, IReadService<BFactorHeader>, IWriteService<BFactorHeader>
    {
        public BFactorHeaderHttpService(HttpClient client) : base(client)
        {
        }

    
        public async Task<IEnumerable<BFactorHeader>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<BFactorHeader>>(
                url: "BFactorHeader/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<BFactorHeader>>("BFactorHeader");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var BFactorHeaders = await GetAsync();
            //return BFactorHeaders.Any();
            return false;
        }

        public async Task<BFactorHeader> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<BFactorHeader>(
                url: $"BFactorHeader/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(BFactorHeader entity)
        {
            await SendRequestAsync<object>(
                url: "BFactorHeader/AddOrUpdate",
                //url: "BFactorHeader",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(BFactorHeader entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"BFactorHeader/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"BFactorHeader/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
