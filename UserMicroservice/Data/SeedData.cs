using UserMicroservice.Models;
namespace UserMicroservice.Data
{
    public static class SeedData
    {
        public static void Initialize(UserDbContext context)
        {
            // Kiểm tra dữ liệu Users
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "Alice", Email = "alice@example.com", Password = "password123", Role = "User" },
                    new User { Username = "Bob", Email = "bob@example.com", Password = "password123", Role = "Admin" },
                    new User { Username = "Charlie", Email = "charlie@example.com", Password = "password123", Role = "User" }
                );
                context.SaveChanges();
            }

            // Kiểm tra dữ liệu FavoriteNovels
            if (!context.FavoriteNovels.Any())
            {
                context.FavoriteNovels.AddRange(
                    new FavoriteNovel { UserID = 1, NovelID = 1 },
                    new FavoriteNovel { UserID = 1, NovelID = 2 },
                    new FavoriteNovel { UserID = 2, NovelID = 3 }
                );
                context.SaveChanges();
            }

            // Kiểm tra dữ liệu FollowedUsers
            if (!context.FollowedUsers.Any())
            {
                context.FollowedUsers.AddRange(
                    new FollowedUser { UserID = 1, FollowedUserID = 2 },
                    new FollowedUser { UserID = 1, FollowedUserID = 3 },
                    new FollowedUser { UserID = 2, FollowedUserID = 3 }
                );
                context.SaveChanges();
            }
        }
    }
}
