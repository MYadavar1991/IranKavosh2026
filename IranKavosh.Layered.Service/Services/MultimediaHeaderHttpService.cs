using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IranKavosh.Layered.Service.Services
{
    //: HttpServiceBase, IReadService<MultiGenre>, IWriteService<MultiGenre>
    public class MultimediaHeaderHttpService : HttpServiceBase, IReadService<MultimediaHeader>, IWriteService<MultimediaHeader>
    {
        public MultimediaHeaderHttpService(HttpClient client) : base(client)
        {
        }

 
        public async Task<IEnumerable<ModelMultiHeader>> GetByModel()
        {
            return await SendRequestAsync<IEnumerable<ModelMultiHeader>>(
                url: "MultimediaHeader/Get",
                method: HttpMethod.Get
            );
        }
        public async Task<IEnumerable<MultimediaHeader>> GetAsync()
        {
            //return await SendRequestAsync<IEnumerable<MultimediaHeader>>(
            //    url: "MultimediaHeader/Get",
            //    method: HttpMethod.Get
            //);
            return null;
        }
        public async Task<long> GetMaxCode()
        {
            return await SendRequestAsync<long>(
                url: "MultimediaHeader/GetMaxCode",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            return await SendRequestAsync<bool>(
                url: $"MultimediaHeader/Any/{id}",
                method: HttpMethod.Get
            );

        }

        public async Task<MultimediaHeader> GetById(Guid id)
        {
            return await SendRequestAsync<MultimediaHeader>(
                url: $"MultimediaHeader/GetById/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task<ModelMultimediaHeader> GetChapterCount(Guid id)
        {
            return await SendRequestAsync<ModelMultimediaHeader>(
                url: $"MultimediaHeader/GetChapterCount/{id}",
                method: HttpMethod.Get
            );
        }
        //
        public async Task AddAsync(MultimediaHeader entity)
        {
            await SendRequestAsync<object>(
                url: "MultimediaHeader/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task UpdateDescriptionAsync(Guid id, string description)
        {
            // PutAsJsonAsync وقتی مقدار string باشد، آن را به "..." تبدیل می‌کند
            var response = await Client.PutAsJsonAsync($"MultiActor/UpdateDescription/{id}", description);
            response.EnsureSuccessStatusCode();
            //if (!response.IsSuccessStatusCode)
            //{
            //    var error = await response.Content.ReadAsStringAsync();
            //    throw new Exception($"{(int)response.StatusCode}: {error}");
            //}
        }
        public async Task UpdateAsync(MultimediaHeader entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"MultimediaHeader/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"MultimediaHeader/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<string> LocationForImage(Guid id)
        {
            return await SendRequestAsync<string>(
                url: $"MultimediaHeader/LocationForImage/{id}",
                method: HttpMethod.Get
            );
            //var response = await Client.GetAsync($"MultimediaHeader/LocationForImage{id}");
            //response.EnsureSuccessStatusCode();
        }
        public async Task<string> Image(Guid id)
        {
            return await SendRequestAsync<string>(
                url: $"MultimediaHeader/Image/{id}",
                method: HttpMethod.Get
            );
            //var response = await Client.GetAsync($"MultimediaHeader/LocationForImage{id}");
            //response.EnsureSuccessStatusCode();
        }


        /// <summary>
        /// دیتای کامل جزئیات فیلم
        /// </summary>
        /// <param name="Header Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModelChapterDetailsFull>> GetByHeaderTranslate(Guid headerId)
        {
            return await SendRequestAsync<IEnumerable<ModelChapterDetailsFull>>(
                url: $"MultimediaHeader/GetByHeaderTranslate/{headerId}",
                method: HttpMethod.Get
            );
        }

    }
}
