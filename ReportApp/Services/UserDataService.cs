using ReportApp.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace ReportApp.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;

        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> AddUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", user);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<User>>
                   (await _httpClient.GetStreamAsync($"api/users"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<User> GetUserId(int userId)
        {
            return await JsonSerializer.DeserializeAsync<User>
               (await _httpClient.GetStreamAsync($"api/users/{userId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
