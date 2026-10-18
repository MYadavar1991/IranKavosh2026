using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class ShareHolderDetailHttpService : HttpServiceBase, IReadService<ShareHolderDetail>, IWriteService<ShareHolderDetail>
    {
        public ShareHolderDetailHttpService(HttpClient client) : base(client)
        {
        }
        public async Task<IEnumerable<ShareHolderDetail>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<ShareHolderDetail>>(
                url: "ShareHolderDetail/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<ShareHolderDetail>>("ShareHolderDetail");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var ShareHolderDetails = await GetAsync();
            //return ShareHolderDetails.Any();
            return false;
        }

        public async Task<ShareHolderDetail> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<ShareHolderDetail>(
                url: $"ShareHolderDetail/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(ShareHolderDetail entity)
        {
            await SendRequestAsync<object>(
                url: "ShareHolderDetail/AddOrUpdate",
                //url: "ShareHolderDetail",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(ShareHolderDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"ShareHolderDetail/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"ShareHolderDetail/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
