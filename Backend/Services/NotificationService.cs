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

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5004/");
        }

        public async Task SendNotification(string message, int userId)
        {
            var request = new { Message = message, UserId = userId };
            var response = await _httpClient.PostAsJsonAsync("api/Notification/send", request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId)
        {
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
