using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class PriceListHttpService : HttpServiceBase, IReadService<PriceList>, IWriteService<PriceList>
    {
        public PriceListHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<PriceList>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<PriceList>>(
                url: "PriceList/Get",
                method: HttpMethod.Get
            );
        }
        public async Task<bool> Any(Guid id)
        {
            return false;
        }
        public async Task<PriceList> GetById(Guid id)
        {
            return await SendRequestAsync<PriceList>(
                url: $"PriceList/GetById/{id}",
                method: HttpMethod.Get
            );
        }
        /// <summary>
        /// ارسال لیست برای حذف مثلا کاغذ ها
        /// </summary>
        /// <param name="lstProductId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModelFullPriceList>> GetFullPriceList(List<Guid>? lstProductId = null)
        {
            return await SendRequestAsync<IEnumerable<ModelFullPriceList>>(
                url: $"PriceList/GetFullPriceList",
                method: HttpMethod.Get,
                body: lstProductId
            );
        }
        public async Task AddAsync(PriceList entity)
        {
            var getStatus = await SendRequestAsync<object>(
                 url: "PriceList/AddOrUpdate",
                 method: HttpMethod.Post,
                 body: entity
             );

        }
        public async Task UpdateAsync(PriceList entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"PriceList/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteAsync(Guid id)
        {
            //var response = await Client.DeleteAsync($"PriceList/{id}");
            await SendRequestAsync<Guid>(
                url: $"PriceList/Delete/{id}",
                method: HttpMethod.Put

            );
            //response.EnsureSuccessStatusCode();
        }
    }
}
