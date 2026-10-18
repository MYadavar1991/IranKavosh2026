using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class LoginHttpService : HttpServiceBase, IReadService<Login>, IWriteService<Login>
    {
        public LoginHttpService(HttpClient client) : base(client)
        {
        }

   
        public async Task<IEnumerable<Login>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<Login>>(
                url: "Login/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<Login>>("Login");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var Logins = await GetAsync();
            //return Logins.Any();
            return false;
        }

        public async Task<Login> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<Login>(
                url: $"Login/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(Login entity)
        {
            await SendRequestAsync<object>(
                url: "Login/AddOrUpdate",
                //url: "Login",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(Login entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"Login/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"Login/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
