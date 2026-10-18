using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;
using IranKavosh.Layered.Service.Middleware;

namespace IranKavosh.Layered.Service.Services
{
    public class MultiActorHttpService : HttpServiceBase, IReadService<MultiActor>, IWriteService<MultiActor>, IWriteServiceActor
    {
        public MultiActorHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MultiActor>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultiActor>>(
                url: "MultiActor/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var items = await GetAsync();
            //return items.Any();
            return false;
        }

        public async Task<MultiActor> GetById(Guid id)
        {
            return await SendRequestAsync<MultiActor>(
                url: $"MultiActor/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultiActor entity)
        {
            await SendRequestAsync<object>(
                url: "MultiActor/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(MultiActor entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //var response = await Client.PutAsJsonAsync($"MultiActor/{entity.Id}", entity);
            var response = await Client.PutAsJsonAsync("MultiActor/Update", entity);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultiActor/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task AddIfNewAsync(List<string> entity)
        {
          //  var getJson = JsonConvert.SerializeObject(entity);
             await SendRequestAsync<object>(
                url: "MultiActor/AddIfNew",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task AddIfNewFullAsync(List<ModelActor> entity)
        {
            //  var getJson = JsonConvert.SerializeObject(entity);
            await SendRequestAsync<object>(
                url: "MultiActor/AddIfNewFull",
                method: HttpMethod.Post,
                body: entity
            );
        }
    }
}
