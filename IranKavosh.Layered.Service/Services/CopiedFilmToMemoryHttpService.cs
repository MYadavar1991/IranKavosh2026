using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;

namespace IranKavosh.Layered.Service.Services
{
    public class CopiedFilmToMemoryHttpService : HttpServiceBase, IReadService<CopiedFilmToMemory>, IWriteService<CopiedFilmToMemory>
    {
        public CopiedFilmToMemoryHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<CopiedFilmToMemory>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<CopiedFilmToMemory>>(
                url: "CopiedFilmToMemory/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            var items = await GetAsync();
            return items?.Any() ?? false;
        }

        public async Task<CopiedFilmToMemory> GetById(Guid id)
        {
            return await SendRequestAsync<CopiedFilmToMemory>(
                url: $"CopiedFilmToMemory/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(CopiedFilmToMemory entity)
        {
            await SendRequestAsync<object>(
                url: "CopiedFilmToMemory/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(CopiedFilmToMemory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"CopiedFilmToMemory/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"CopiedFilmToMemory/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
