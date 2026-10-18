using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using Newtonsoft.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class FundHttpService : HttpServiceBase, IReadService<Fund>, IWriteService<Fund>, IWriteServiceFund<Fund>
    {
        public FundHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<Fund>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<Fund>>(
                url: "Fund/Get",
                method: HttpMethod.Get
            );
        }
        public async Task<IEnumerable<Fund>> GetByDate(DateTime date)
        {
            return await SendRequestAsync<IEnumerable<Fund>>(
                url: "Fund/GetByDate",
                method: HttpMethod.Post,
                body: date.ToString("yyyy/MM/dd HH:mm:ss")

            );
        }
        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<Fund> GetById(Guid id)
        {
            return await SendRequestAsync<Fund>(
                url: $"Fund/GetById/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task<Fund> GetByGlobalId(Guid id)
        {
            return await SendRequestAsync<Fund>(
                url: $"Fund/GetByGlobalId/{id}",
                method: HttpMethod.Get
            );
        }
        //
        public async Task AddAsync(Fund entity)
        {
            await SendRequestAsync<object>(
                url: "Fund/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }
        public async Task AddByGroupId(Fund entity)
        {
            await SendRequestAsync<object>(
                url: "Fund/AddByGroupId",
                method: HttpMethod.Post,
                body: entity
            );
        }//AddByGroupId
        public async Task UpdateRangeDateAsync(DateTime date)
        {
            //var getJson=
            await SendRequestAsync<object>(
                url: $"Fund/UpdateCalc",
                method: HttpMethod.Put,
                body: date
            );

        }
        public async Task UpdateAsync(Fund entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"Fund/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            //var response = await Client.DeleteAsync($"Fund/{id}");
            await SendRequestAsync<Guid>(
                url: $"Fund/Delete/{id}",
                method: HttpMethod.Put

            );
            //response.EnsureSuccessStatusCode();
        }
    }
}
