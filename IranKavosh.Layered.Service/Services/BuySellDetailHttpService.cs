using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class BuySellDetailHttpService : HttpServiceBase, IReadService<BuySellDetail>, IWriteService<BuySellDetail>
    {
        public BuySellDetailHttpService(HttpClient client) : base(client)
        {
        }
        public async Task<IEnumerable<BuySellDetail>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<BuySellDetail>>(
                url: "BuySellDetail/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<BuySellDetail>>("BuySellDetail");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var BuySellDetails = await GetAsync();
            //return BuySellDetails.Any();
            return false;
        }
        //GetByGroupId
        public async Task<BuySellDetail> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<BuySellDetail>(
                url: $"BuySellDetail/GetById/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task<IEnumerable<BuySellDetail>> GetByGroupId(Guid id)
        {
            // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<IEnumerable<BuySellDetail>>(
                url: $"BuySellDetail/GetByGroupId/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(BuySellDetail entity)
        {
            await SendRequestAsync<object>(
                url: "BuySellDetail/AddOrUpdate",
                //url: "BuySellDetail",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(BuySellDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"BuySellDetail/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"BuySellDetail/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
