using Microsoft.EntityFrameworkCore;
using NotificationMicroservice.Models;

namespace NotificationMicroservice.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
            : base(options)
        {
        }

        // DbSet đại diện cho bảng Notifications trong database
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired();

                // Sử dụng kiểu timestamp thay vì datetime
                entity.Property(e => e.Timestamp)
                      .HasColumnType("timestamp")  // Đổi kiểu dữ liệu thành timestamp
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Giá trị mặc định
            });

            modelBuilder.Entity<Notification>().HasData(
                new Notification { Id = 1, UserId = 001, Message = "Welcome to the app!", Timestamp = DateTime.UtcNow, IsRead = false }
            );
        }
    }
}
