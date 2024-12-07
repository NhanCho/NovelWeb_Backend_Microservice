using Microsoft.EntityFrameworkCore;
using NovelMicroservice.Models;

namespace NovelMicroservice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet cho mỗi model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Novel> Novels { get; set; }
        public DbSet<Chapter> Chapters { get; set; }

        // Cấu hình quan hệ giữa các bảng
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ giữa Category và Novel
            modelBuilder.Entity<Novel>(entity =>
            {
                entity.HasKey(n => n.NovelID); // Khóa chính
                entity.Property(n => n.Name).IsRequired();
                entity.Property(n => n.Author).IsRequired();
                entity.Property(n => n.Description).IsRequired(false); // Mô tả có thể null
                entity.Property(n => n.CategoryID).IsRequired(); // CategoryID là bắt buộc
                entity.HasIndex(n => n.CategoryID); // Tạo index trên CategoryID

            });
            // Cấu hình bảng Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryID); // Khóa chính
                entity.Property(c => c.Name).IsRequired();
            });

            // Cấu hình bảng Chapter
            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.HasKey(ch => ch.ChapterID); // Khóa chính
                entity.Property(ch => ch.ChapterNumber).IsRequired();
                entity.Property(ch => ch.Content).IsRequired();
                entity.Property(ch => ch.NovelID).IsRequired(); // NovelID là bắt buộc
                entity.HasIndex(ch => ch.NovelID); // Tạo index trên NovelID

            });
        }
    }
}
