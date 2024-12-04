using NotificationMicroservice.Models;
using NotificationMicroservice.Repositories;

namespace NotificationMicroservice.Services
{
    public interface INotificationObserver
    {
        void Update(Notification notification);
    }

    public class NotificationService
    {
        private readonly NotificationRepository _repository;
        private readonly INotificationFactory _factory;
        private readonly List<INotificationObserver> _observers = new();

        public NotificationService(NotificationRepository repository, INotificationFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        // Đăng ký observer
        public void RegisterObserver(INotificationObserver observer)
        {
            _observers.Add(observer);
        }

        // Hủy đăng ký observer
        public void UnregisterObserver(INotificationObserver observer)
        {
            _observers.Remove(observer);
        }

        // Gửi thông báo sử dụng Factory Method
        public async Task SendNotification(string message, int userId)
        {
            var notification = _factory.CreateNotification(message, userId);
            await _repository.AddNotification(notification);
            NotifyObservers(notification);
        }

        // Lấy danh sách thông báo của người dùng
        public async Task<List<Notification>> GetNotificationsByUserId(int userId)
        {
            return await _repository.GetNotificationsByUserId(userId);
        }

        // Đánh dấu thông báo đã đọc
        public async Task MarkAsReadAsync(int id)
        {
            await _repository.MarkAsRead(id);
        }

        // Xóa thông báo
        public async Task DeleteNotification(int id)
        {
            await _repository.DeleteNotification(id);
        }

        // Gửi thông báo tới tất cả observers
        private void NotifyObservers(Notification notification)
        {
            foreach (var observer in _observers)
            {
                observer.Update(notification);
            }
        }
    }
}
