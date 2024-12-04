using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models;
namespace UserMicroservice.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FollowedUser> FollowedUsers { get; set; }
        public DbSet<FavoriteNovel> FavoriteNovels { get; set; }
    }
}
