using System;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;

namespace IranKavosh.Layered.Service.Services
{
    public class HddMemoryPersonHttpService : HttpServiceBase, IReadService<HddMemoryPerson>, IWriteService<HddMemoryPerson>
    {
        public HddMemoryPersonHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<HddMemoryPerson>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<HddMemoryPerson>>(
                url: "HddMemoryPerson/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            var list = await GetAsync();
            return list?.Any() ?? false;
        }

        public async Task<HddMemoryPerson> GetById(Guid id)
        {
            return await SendRequestAsync<HddMemoryPerson>(
                url: $"HddMemoryPerson/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(HddMemoryPerson entity)
        {
            await SendRequestAsync<object>(
                url: "HddMemoryPerson/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(HddMemoryPerson entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"HddMemoryPerson/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"HddMemoryPerson/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}