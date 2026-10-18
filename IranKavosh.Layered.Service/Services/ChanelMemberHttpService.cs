using System.Net.Http.Json;
using IranKavosh.Layered.Model.Table_Bot;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class ChanelMemberHttpService : HttpServiceBase, IReadService<ChanelMember>, IWriteService<ChanelMember>
    {
        public ChanelMemberHttpService(HttpClient client) : base(client)
        {
        }


        public async Task<IEnumerable<ChanelMember>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<ChanelMember>>(
                url: "ChanelMember/Get",
                method: HttpMethod.Get
            );

            // return await Client.GetFromJsonAsync<IEnumerable<ChanelMember>>("ChanelMember");
        }
        public async Task<bool> Any(Guid id)
        {
            // نمونه پیاده‌سازی
            //var ChanelMembers = await GetAsync();
            //return ChanelMembers.Any();
            return false;
        }

        public async Task<ChanelMember> GetById(Guid id)
        {
           // var getJson= JsonConvert.SerializeObject(id);
            return await SendRequestAsync<ChanelMember>(
                url: $"ChanelMember/GetById/{id}",
                method: HttpMethod.Get
            );
        }

        public async Task AddAsync(ChanelMember entity)
        {
            await SendRequestAsync<object>(
                url: "ChanelMember/AddOrUpdate",
                //url: "ChanelMember",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task AddReferral(ModelBot.ModelReferral entity)
        {
            await SendRequestAsync<object>(
                url: "ChanelMember/AddReferral",
                //url: "ChanelMember",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task UpdateAsync(ChanelMember entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"ChanelMember/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"ChanelMember/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
