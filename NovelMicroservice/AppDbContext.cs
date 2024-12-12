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
                new Category { CategoryID = 1, Name = "Children", ImageUrl = "https://media.istockphoto.com/id/177321629/vi/anh/nh%C3%B3m-tr%E1%BA%BB-em-c%C3%B3-d%E1%BA%A5u-hi%E1%BB%87u-gi%C6%A1-ng%C3%B3n-tay-c%C3%A1i-l%C3%AAn.jpg?s=2048x2048&w=is&k=20&c=osGSOiFdJ-A3QB5UOD06R22vFACOAc9A5MYddfV4EBs=" },
                new Category { CategoryID = 2, Name = "Horror", ImageUrl = "https://media.istockphoto.com/id/1198829958/photo/group-of-five-scary-figures-in-hooded-cloaks-in-the-dark.jpg?s=612x612&w=0&k=20&c=Vjx6Kz6zpdqPrUr1RAUyXWwcOlsy64vd6_ENdPl-r0E=" },
                new Category { CategoryID = 3, Name = "Fantasy", ImageUrl = "https://media.istockphoto.com/id/688410346/vector/chinese-style-fantasy-scenes.jpg?s=612x612&w=0&k=20&c=r3skS5InspYQ7EqUCCzUzU3QHcwRwD6mNRbDpP8sIG4=" }
            );

            // Seed Novel Data
            modelBuilder.Entity<Novel>().HasData(
                new Novel { NovelID = 1, Name = "The Little Prince", Author = "Antoine de Saint-Exupéry", Description = "A timeless story of a young prince and his adventures.", CategoryID = 1, ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/71OZY035QKL.jpg" },
                new Novel { NovelID = 2, Name = "Dracula", Author = "Bram Stoker", Description = "The classic vampire tale that started it all.", CategoryID = 2, ImageUrl = "https://cdn.kobo.com/book-images/88a05cf1-a3b6-461b-a8f7-f0e25b06274a/1200/1200/False/dracula-bram-stoker.jpg" },
                new Novel { NovelID = 3, Name = "Harry Potter", Author = "J.K. Rowling", Description = "A young wizard's journey through magic and friendship.", CategoryID = 3, ImageUrl = "https://nhasachphuongnam.com/images/detailed/160/81YOuOGFCJL.jpg" }
            );

            // Seed Chapter Data
            modelBuilder.Entity<Chapter>().HasData(
                new Chapter { ChapterID = 1, ChapterNumber = 1, Content = "Once upon a time...", NovelID = 1 },
                new Chapter { ChapterID = 2, ChapterNumber = 2, Content = "The journey begins.", NovelID = 1 },
                new Chapter { ChapterID = 3, ChapterNumber = 1, Content = "In the shadows of the night.", NovelID = 2 },
                new Chapter { ChapterID = 4, ChapterNumber = 1, Content = "The boy who lived.", NovelID = 3 }
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
                entity.Property(c => c.ImageUrl).IsRequired(); // Đảm bảo ImageUrl là bắt buộc
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
