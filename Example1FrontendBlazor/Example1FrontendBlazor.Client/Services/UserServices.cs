using One.Shared.Model;
using System.Net.Http.Json;

namespace One.Frontend.Client
{
    public class UserWebService
    {
        private readonly HttpClient _httpClient;

        public UserWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("api/users");
        }
    }
}
