using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class ShareHolderHeaderHttpService : HttpServiceBase, IReadService<ShareHolderHeader>, IWriteService<ShareHolderHeader>
    {
        public ShareHolderHeaderHttpService(HttpClient client) : base(client)
        {
        }
        public async Task<IEnumerable<ShareHolderHeader>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<ShareHolderHeader>>(
                url: "ShareHolderHeader/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<ShareHolderHeader>>("ShareHolderHeader");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var ShareHolderHeaders = await GetAsync();
            //return ShareHolderHeaders.Any();
            return false;
        }

        public async Task<ShareHolderHeader> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<ShareHolderHeader>(
                url: $"ShareHolderHeader/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(ShareHolderHeader entity)
        {
            await SendRequestAsync<object>(
                url: "ShareHolderHeader/AddOrUpdate",
                //url: "ShareHolderHeader",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(ShareHolderHeader entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"ShareHolderHeader/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"ShareHolderHeader/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
