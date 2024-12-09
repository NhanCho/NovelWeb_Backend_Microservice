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

            // Seed Category Data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, Name = "Fantasy" },
                new Category { CategoryID = 2, Name = "Horror" }
            );

            // Seed Novel Data
            modelBuilder.Entity<Novel>().HasData(
                new Novel { NovelID = 1, Name = "Little Adventures", Author = "Author A", Description = "A fun tale for kids", CategoryID = 1 },
                new Novel { NovelID = 2, Name = "Haunted House", Author = "Author B", Description = "A scary story", CategoryID = 2 }
            );

            // Seed Chapter Data
            modelBuilder.Entity<Chapter>().HasData(
                new Chapter { ChapterID = 1, ChapterNumber = 1, Content = "Once upon a time...", NovelID = 1 },
                new Chapter { ChapterID = 2, ChapterNumber = 1, Content = "It was a dark and stormy night...", NovelID = 2 }
            );
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
