using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class PersonalHttpService : HttpServiceBase, IReadService<Personal>, IWriteService<Personal>
    {
        public PersonalHttpService(HttpClient client) : base(client)
        {
        }

        #region Bot

        public async Task<bool> BotAnyUserId(long userId)
        {
            return await SendRequestAsync<bool>(
                url: $"Personal/BotAnyUserId/{userId}",
                method: HttpMethod.Get
            );
        }   
        public async Task AddFromBotAsync(Personal entity)
        {
            await SendRequestAsync<object>(
                url: "Personal/BotUpdateUserId",
                method: HttpMethod.Post,
                body: entity
            );
        }
        #endregion
        public async Task<IEnumerable<Personal>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<Personal>>(
                url: "Personal/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<Personal>>("Personal");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var personals = await GetAsync();
            //return personals.Any();
            return false;
        }
        //UpdatePassword
        //     public async Task<IActionResult> UpdatePassword(long botUserId, string pass)
        public async Task<Personal> UpdatePassword(long botUserId, string pass)
        {
            // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<Personal>(
                url: $"Personal/UpdatePassword/{botUserId}/{pass}",
                method: HttpMethod.Post
            );
        }
        public async Task<Personal> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<Personal>(
                url: $"Personal/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(Personal entity)
        {
            await SendRequestAsync<object>(
                url: "Personal/AddOrUpdate",
                //url: "Personal",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(Personal entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"Personal/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"Personal/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
