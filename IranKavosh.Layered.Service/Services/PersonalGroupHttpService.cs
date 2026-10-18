using System.Net.Http.Json;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.HttpBase;
using IranKavosh.Layered.Service.Interface.IranKavosh.Layered.Service.Interfaces;

namespace IranKavosh.Layered.Service.Services
{
    public class PersonalGroupHttpService : HttpServiceBase, IReadService<PersonalGroup>, IWriteService<PersonalGroup>
    {
        public PersonalGroupHttpService(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<PersonalGroup>> GetAsync()
        {
            //try
            //{
                return await SendRequestAsync<IEnumerable<PersonalGroup>>(
                    url: "PersonalGroup/Get",
                    method: HttpMethod.Get
                );
            //}
            //catch (Exception e)
            //{
                
            //}

            return null;
        }

        public async Task<bool> Any(Guid id)
        {
            var groups = await GetAsync();
            return groups.Any();
        }

        public async Task<PersonalGroup> GetById(Guid id)
        {
            //var groups = await GetAsync();
            //return groups.Where(g => g.Id == id);
            return null;
        }

        public async Task AddAsync(PersonalGroup entity)
        {
            await SendRequestAsync<object>(
                url: "PersonalGroup/AddOrUpdate",
                method: HttpMethod.Post,
                body: entity
            );
        }

        public async Task UpdateAsync(PersonalGroup entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var response = await Client.PutAsJsonAsync($"PersonalGroup/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"PersonalGroup/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
