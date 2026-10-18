using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Model.Tables.BOT;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class BFactorDetailHttpService : HttpServiceBase, IReadService<BFactorDetail>, IWriteService<BFactorDetail>
    {
        public BFactorDetailHttpService(HttpClient client) : base(client)
        {
        }

     
        public async Task<IEnumerable<BFactorDetail>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<BFactorDetail>>(
                url: "BFactorDetail/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<BFactorDetail>>("BFactorDetail");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var BFactorDetails = await GetAsync();
            //return BFactorDetails.Any();
            return false;
        }

        public async Task<BFactorDetail> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<BFactorDetail>(
                url: $"BFactorDetail/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(BFactorDetail entity)
        {
            await SendRequestAsync<object>(
                url: "BFactorDetail/AddOrUpdate",
                //url: "BFactorDetail",
                method: HttpMethod.Post,
                body: entity
            );
        }

   


        public async Task AddRange(List<BFactorDetail> entity,Guid id)
        {
            await SendRequestAsync<object>(
                url: $"BFactorDetail/AddRange/{id}",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task UpdateAsync(BFactorDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"BFactorDetail/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"BFactorDetail/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
