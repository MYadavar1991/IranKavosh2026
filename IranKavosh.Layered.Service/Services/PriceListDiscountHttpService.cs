using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class PriceListDiscountHttpService : HttpServiceBase, IReadService<PriceListDiscount>, IWriteService<PriceListDiscount>
    {
        public PriceListDiscountHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<PriceListDiscount>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<PriceListDiscount>>(
                url: "PriceListDiscount/Get",
                method: HttpMethod.Get
            );
        }
        public async Task<bool> Any(Guid id)
        {
            return false;
        }
        public async Task<PriceListDiscount> GetById(Guid id)
        {
            return await SendRequestAsync<PriceListDiscount>(
                url: $"PriceListDiscount/GetById/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task<PriceListDiscount> GetDiscount(Guid id)
        {
            return await SendRequestAsync<PriceListDiscount>(
                url: $"PriceListDiscount/GetDiscount/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task AddAsync(PriceListDiscount entity)
        {
            var getStatus = await SendRequestAsync<object>(
                 url: "PriceListDiscount/AddOrUpdate",
                 method: HttpMethod.Post,
                 body: entity
             );
           
        }
        public async Task UpdateAsync(PriceListDiscount entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"PriceListDiscount/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteAsync(Guid id)
        {
            //var response = await Client.DeleteAsync($"PriceListDiscount/{id}");
            await SendRequestAsync<Guid>(
                url: $"PriceListDiscount/Delete/{id}",
                method: HttpMethod.Put
            );
            //response.EnsureSuccessStatusCode();
        }
    }
}
