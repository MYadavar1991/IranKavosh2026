using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class MultiLocationHttpService : HttpServiceBase, IReadService<MultiLocation>, IWriteService<MultiLocation>
    {
        public MultiLocationHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MultiLocation>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultiLocation>>(
                url: "MultiLocation/Get",
                method: HttpMethod.Get
            );
        }
     
        public async Task<IEnumerable<ModelSizeHDD>> GetSizeHDD()
        {
            return await SendRequestAsync<IEnumerable<ModelSizeHDD>>(
                url: "MultiLocation/GetSizeHDD",
                method: HttpMethod.Get
            );
        }
        public async Task<bool> Any(Guid id)
        {
            // Example implementation
            // var locations = await GetAsync();
            // return locations.Any();
            return false;
        }

        public async Task<MultiLocation> GetById(Guid id)
        {
            return await SendRequestAsync<MultiLocation>(
                url: $"MultiLocation/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultiLocation entity)
        {
            await SendRequestAsync<object>(
                url: "MultiLocation/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(MultiLocation entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultiLocation/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultiLocation/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
