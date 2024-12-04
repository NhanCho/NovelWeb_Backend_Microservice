using Microsoft.AspNetCore.Mvc;
using NotificationMicroservice.Models;
using NotificationMicroservice.Services;

namespace NotificationMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _service;

        public NotificationController(NotificationService service)
        {
            _service = service;
        }

        // Gửi thông báo
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            try
            {
                await _service.SendNotification(request.Message, request.UserId);
                return Ok(new { message = "Notification sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Lấy thông báo của một user
        [HttpGet("{userId}/notifications")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            try
            {
                var notifications = await _service.GetNotificationsByUserId(userId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // Đánh dấu thông báo là đã đọc
        [HttpPatch("{notificationId}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            try
            {
                await _service.MarkAsReadAsync(notificationId);
                return Ok(new { message = "Notification marked as read." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
