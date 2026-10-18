using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class ProductHttpService : HttpServiceBase, IReadService<Product>, IWriteService<Product>
    {
        public ProductHttpService(HttpClient client) : base(client)
        {
        }
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<Product>>(
                url: "Product/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<Product>>("Product");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var Products = await GetAsync();
            //return Products.Any();
            return false;
        }

        public async Task<Product> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<Product>(
                url: $"Product/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(Product entity)
        {
            await SendRequestAsync<object>(
                url: "Product/AddOrUpdate",
                //url: "Product",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"Product/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"Product/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
