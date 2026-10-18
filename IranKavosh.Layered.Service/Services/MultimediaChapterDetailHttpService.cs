using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;
using System.Net.Http.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class MultimediaChapterDetailHttpService : HttpServiceBase, IReadService<MultimediaChapterDetail>, IWriteService<MultimediaChapterDetail>
    {
        public MultimediaChapterDetailHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MultimediaChapterDetail>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<MultimediaChapterDetail>>(
                url: "MultimediaChapterDetail/Get",
                method: HttpMethod.Get
            );
        }
        public async Task<IEnumerable<MultimediaChapterDetail>> GetByHeaderChapter(Guid id)
        {
            return await SendRequestAsync<IEnumerable<MultimediaChapterDetail>>(
                url: $"MultimediaChapterDetail/GetByHeaderChapter/{id}",
                method: HttpMethod.Get
            );
        }
        //GetByHeaderTranslate
        public async Task<IEnumerable<MultimediaChapterDetail>> GetByHeader(Guid id)
        {
            return await SendRequestAsync<IEnumerable<MultimediaChapterDetail>>(
                url: $"MultimediaChapterDetail/GetByHeader/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task<long> GetPrice(Guid id)
        {
            return await SendRequestAsync<long>(
                url: $"MultimediaChapterDetail/GetPrice/{id}",
                method: HttpMethod.Get
            );
        }

        /// <summary>
        /// دیتای کامل جزئیات فیلم
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModelChapterDetailsFull>> GetByHeaderTranslate(List<ModelGuidFaEn> ids)
        {
            return await SendRequestAsync<IEnumerable<ModelChapterDetailsFull>>(
                url: $"MultimediaChapterDetail/GetByHeaderTranslate",
                method: HttpMethod.Get,
                body: ids

            );
        }
        public async Task<long> GetMaxCode(Guid id)
        {
            return await SendRequestAsync<long>(
                url: $"MultimediaChapterDetail/GetMaxCode/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<MultimediaChapterDetail> GetById(Guid id)
        {
            return await SendRequestAsync<MultimediaChapterDetail>(
                url: $"MultimediaChapterDetail/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(MultimediaChapterDetail entity)
        {
            await SendRequestAsync<object>(
                url: "MultimediaChapterDetail/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }
        //public async Task AddChapterCounts(Guid headerChapterId, int chapterCount)
        public async Task<IEnumerable<MultimediaChapterDetail>> AddChapterCounts(Guid headerChapterId, int chapterCount)
        {
            var body = new
            {
                headerChapterId,  // نام فیلد باید با کلاس سرور یکی باشد  
                chapterCount
            };
            var getData = await SendRequestAsync<IEnumerable<MultimediaChapterDetail>>(
                 url: $"MultimediaChapterDetail/AddChapterCounts/{headerChapterId}/{chapterCount}",
                 method: HttpMethod.Post,
                 body: body
             );
            return getData;
        }
        //IsWebDl
        public async Task UpdateAsync(MultimediaChapterDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultimediaChapterDetail/{entity.Id}", entity);

            response.EnsureSuccessStatusCode();
        }
        public async Task<object> IsWebDl(Guid id, bool value)
        {

            return await SendRequestAsync<bool>(
                url: $"MultimediaChapterDetail/IsWebDl/{id}",
                method: HttpMethod.Put,
                body: value
            );
        }
        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultimediaChapterDetail/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
