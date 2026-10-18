using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;

namespace IranKavosh.Layered.Service.Services
{
    public class MultiGenreHttpService : HttpServiceBase, IReadService<MultiGenre>, IWriteService<MultiGenre>
    {
        public MultiGenreHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MultiGenre>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultiGenre>>(
                url: "MultiGenre/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<MultiGenre> GetById(Guid id)
        {
            return await SendRequestAsync<MultiGenre>(
                url: $"MultiGenre/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultiGenre entity)
        {
            await SendRequestAsync<object>(
                url: "MultiGenre/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task AddIfNewAsync(List<ModelGenre> entity)
        {
            //  var getJson = JsonConvert.SerializeObject(entity);
            await SendRequestAsync<object>(
                url: "MultiGenre/AddIfNew",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task UpdateAsync(MultiGenre entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultiGenre/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultiGenre/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
