using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class MultimediaHeaderChapterHttpService : HttpServiceBase, IReadService<MultimediaHeaderChapter>, IWriteService<MultimediaHeaderChapter>
    {
        public MultimediaHeaderChapterHttpService(HttpClient client) : base(client)
        {

        }

        public async Task<IEnumerable<MultimediaHeaderChapter>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultimediaHeaderChapter>>(
                url: "MultimediaHeaderChapter/Get",
                method: HttpMethod.Get
            );
        }
        public async Task<IEnumerable<MultimediaHeaderChapter>> GetByHeader(Guid id)
        {
            return await SendRequestAsync<IEnumerable<MultimediaHeaderChapter>>(
                url: $"MultimediaHeaderChapter/GetByHeader/{id}",
                method: HttpMethod.Get
            );
        }
        
        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<MultimediaHeaderChapter> GetById(Guid id)
        {
            return await SendRequestAsync<MultimediaHeaderChapter>(
                url: $"MultimediaHeaderChapter/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultimediaHeaderChapter entity)
        {
            await SendRequestAsync<object>(
                url: "MultimediaHeaderChapter/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(MultimediaHeaderChapter entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultimediaHeaderChapter/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultimediaHeaderChapter/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<IEnumerable<MultimediaHeaderChapter>> GetHeaderChapter(Guid id)
        {
            return await SendRequestAsync<IEnumerable<MultimediaHeaderChapter>>(
                url: $"MultimediaHeaderChapter/GetHeaderChapter/{id}",
                method: HttpMethod.Get
            );
        }
    }
}
