using Backend.Models;
using System.Text;

namespace Backend.Services
{
    public interface INotificationService
    {
        Task SendNotification(string message, int userId);
        Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId);
        Task MarkAsRead(int notificationId);
    }

    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _userHttpClient;

        public NotificationService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5004/");
            _userHttpClient = httpClientFactory.CreateClient("UserService");
        }

        public async Task SendNotification(string message, int userId)
        {
            var userResponse = await _userHttpClient.GetAsync($"api/User/GetUserById/{userId}");
            if (!userResponse.IsSuccessStatusCode)
            {
                throw new Exception("UserId doesn't exist! Can't send notification!");
            }

            var request = new { Message = message, UserId = userId };
            var response = await _httpClient.PostAsJsonAsync("api/Notification/send", request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId)
        {
            var userResponse = await _userHttpClient.GetAsync($"api/User/GetUserById/{userId}");
            if (!userResponse.IsSuccessStatusCode)
            {
                throw new Exception("UserId doesn't exist! Can't get notification!");
            }

            var response = await _httpClient.GetAsync($"api/Notification/notifications/{userId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<Notification>>();
        }

        public async Task MarkAsRead(int notificationId)
        {
            var response = await _httpClient.PatchAsync($"api/Notification/mark-as-read/{notificationId}", null);

            response.EnsureSuccessStatusCode();
        }
    }
}
