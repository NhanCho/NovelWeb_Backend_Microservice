using Backend.Services;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _userHttpClient;

        public NotificationController(INotificationService notificationService, HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _notificationService = notificationService;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5004/");
            _userHttpClient = httpClientFactory.CreateClient("UserService");
        }

        [HttpPost("send-to-all")]
        public async Task<IActionResult> SendNotificationToAll([FromBody] SendToAllRequest request)
        {
            try
            {
                // Lấy danh sách tất cả người dùng từ User Microservice
                var userResponse = await _userHttpClient.GetAsync("api/User/GetUsers");

                if (!userResponse.IsSuccessStatusCode)
                {
                    return StatusCode(500, new { error = "Failed to get users!" });
                }

                var users = await userResponse.Content.ReadFromJsonAsync<List<UserDto>>();

                if (users == null || !users.Any())
                {
                    return BadRequest(new { message = "No users found!" });
                }

                // Gửi thông báo cho tất cả người dùng
                foreach (var user in users)
                {
                    // Gửi thông báo cho từng người dùng qua Notification Microservice
                    await _notificationService.SendNotification(request.Message, user.UserID);
                }

                return Ok(new { message = "Notification sent to all users." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            try
            {
                var userResponse = await _userHttpClient.GetAsync($"api/User/GetUserById/{request.UserId}");
                if (!userResponse.IsSuccessStatusCode)
                {
                    return StatusCode(404, new { error = "User doesn't exist!" });
                }

                var user = await userResponse.Content.ReadFromJsonAsync<UserDto>();

                await _notificationService.SendNotification(request.Message, request.UserId);
                return Ok(new { message = $"Notification sent successfully to {user.UserID} - {user.Username}!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("notifications/{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            try
            {
                var notifications = await _notificationService.GetNotificationsByUserId(userId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPatch("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            try
            {
                await _notificationService.MarkAsRead(notificationId);
                return Ok(new { message = "Notification marked as read." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
