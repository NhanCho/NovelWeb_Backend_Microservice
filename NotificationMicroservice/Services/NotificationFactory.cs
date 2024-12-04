using NotificationMicroservice.Models;

namespace NotificationMicroservice.Services
{
    public class NotificationFactory : INotificationFactory
    {
        public Notification CreateNotification(string message, int userId)
        {
            return new Notification
            {
                Message = message,
                UserId = userId,
                IsRead = false,
                Timestamp = DateTime.UtcNow
            };
        }
    }
}
