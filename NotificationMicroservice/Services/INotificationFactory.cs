using NotificationMicroservice.Models;

namespace NotificationMicroservice.Services
{
    public interface INotificationFactory
    {
        Notification CreateNotification(string message, int userId);
    }
}
