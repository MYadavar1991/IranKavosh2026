using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;
using IranKavosh.Layered.Service.Middleware;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace IranKavosh.Layered.Service.Services
{
    public class DefinitiveAccountHttpService : HttpServiceBase, IReadService<DefinitiveAccount>, IWriteService<DefinitiveAccount>
    {
        public DefinitiveAccountHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<DefinitiveAccount>> GetAsync()
        {
            return await SendRequestAsync<IEnumerable<DefinitiveAccount>>(
                url: "DefinitiveAccount/Get",
                method: HttpMethod.Get
            );
        }

        public async Task<bool> Any(Guid id)
        {
            return false;
        }

        public async Task<DefinitiveAccount> GetById(Guid id)
        {
            return await SendRequestAsync<DefinitiveAccount>(
                url: $"DefinitiveAccount/GetById/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task<IEnumerable<ModelDebtor>> GetByDebtor()
        {
            return await SendRequestAsync<IEnumerable<ModelDebtor>>(
                url: $"DefinitiveAccount/GetByDebtor",
                method: HttpMethod.Get
            );
        }
        //GetByPersonCalc
        public async Task<long> GetByPersonCalc(Guid id)
        {
            return await SendRequestAsync<long>(
                url: $"DefinitiveAccount/GetByPersonCalc/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task<IEnumerable<DefinitiveAccount>> GetByPersonId(Guid id)
        {
            return await SendRequestAsync< IEnumerable<DefinitiveAccount>>(
                url: $"DefinitiveAccount/GetByPersonId/{id}",
                method: HttpMethod.Get
            );
        }
        public async Task AddAsync(DefinitiveAccount entity)
        {
            await SendRequestAsync<object>(
                url: "DefinitiveAccount/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(DefinitiveAccount entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"DefinitiveAccount/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"DefinitiveAccount/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}