using NotificationMicroservice.Models;
using NotificationMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace NotificationMicroservice.Repositories
{
    public class NotificationRepository
    {
        private readonly NotificationDbContext _context;

        public NotificationRepository(NotificationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách thông báo của một người dùng
        public async Task<List<Notification>> GetNotificationsByUserId(int userId)
        {
            return await _context.Notifications
                                 .Where(n => n.UserId == userId)
                                 .OrderByDescending(n => n.Timestamp)
                                 .ToListAsync();
        }

        // Lấy thông báo theo Id
        public async Task<Notification> GetNotificationById(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        // Tạo một thông báo mới
        public async Task AddNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        // Đánh dấu thông báo đã đọc
        public async Task MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        // Xóa một thông báo
        public async Task DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
