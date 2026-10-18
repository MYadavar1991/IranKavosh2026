using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class MultiCountryHttpService : HttpServiceBase, IReadService<MultiCountry>, IWriteService<MultiCountry>
    {
        public MultiCountryHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MultiCountry>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultiCountry>>(
                url: "MultiCountry/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<MultiCountry> GetById(Guid id)
        {
            return await SendRequestAsync<MultiCountry>(
                url: $"MultiCountry/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultiCountry entity)
        {
            await SendRequestAsync<object>(
                url: "MultiCountry/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(MultiCountry entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultiCountry/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultiCountry/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
