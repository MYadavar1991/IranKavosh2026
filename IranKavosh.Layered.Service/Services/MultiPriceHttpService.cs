using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class MultiPriceHttpService : HttpServiceBase, IReadService<MultiPrice>, IWriteService<MultiPrice>
    {
        public MultiPriceHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MultiPrice>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultiPrice>>(
                url: "MultiPrice/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<MultiPrice> GetById(Guid id)
        {
            return await SendRequestAsync<MultiPrice>(
                url: $"MultiPrice/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultiPrice entity)
        {
            await SendRequestAsync<object>(
                url: "MultiPrice/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(MultiPrice entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultiPrice/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultiPrice/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
