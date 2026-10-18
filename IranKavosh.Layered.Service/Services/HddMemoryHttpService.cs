using System.Net.Http.Json;
using System.Linq;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;

namespace IranKavosh.Layered.Service.Services
{
    public class HddMemoryHttpService : HttpServiceBase, IReadService<HddMemory>, IWriteService<HddMemory>
    {
        public HddMemoryHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<HddMemory>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<HddMemory>>(
                url: "HddMemory/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            var list = await GetAsync();
            return list?.Any() ?? false;
        }

        public async Task<HddMemory> GetById(Guid id)
        {
            return await SendRequestAsync<HddMemory>(
                url: $"HddMemory/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(HddMemory entity)
        {
            await SendRequestAsync<object>(
                url: "HddMemory/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task AddByStableSerialAsync(HddMemory entity)
        {
            await SendRequestAsync<object>(
                url: "HddMemory/AddBySerialStable",   
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task UpdateAsync(HddMemory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"HddMemory/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"HddMemory/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
