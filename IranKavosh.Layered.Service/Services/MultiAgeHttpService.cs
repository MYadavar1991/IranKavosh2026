using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class MultiAgeHttpService : HttpServiceBase, IReadService<MultiAge>, IWriteService<MultiAge>
    {
        public MultiAgeHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MultiAge>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultiAge>>(
                url: "MultiAge/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            // Example implementation
            // var items = await GetAsync();
            // return items.Any();
            return false;
        }

        public async Task<MultiAge> GetById(Guid id)
        {
            return await SendRequestAsync<MultiAge>(
                url: $"MultiAge/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultiAge entity)
        {
            await SendRequestAsync<object>(
                url: "MultiAge/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(MultiAge entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultiAge/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultiAge/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
