using UserMicroservice.Data;
using UserMicroservice.Models;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Repositories.Implementations
{
    public class FollowedUserRepository : IFollowedUserRepository
    {
        private readonly UserDbContext _context;

        public FollowedUserRepository(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FollowedUser> GetFollowedUsersByUserId(int userId)
        {
            return _context.FollowedUsers.Where(f => f.UserID == userId).ToList();
        }

        public void AddFollowedUser(FollowedUser followedUser)
        {
            // Kiểm tra trùng lặp
            if (_context.FollowedUsers.Any(f => f.UserID == followedUser.UserID && f.FollowedUserID == followedUser.FollowedUserID))
            {
                throw new Exception("This relationship already exists.");
            }
            _context.FollowedUsers.Add(followedUser);
            _context.SaveChanges();
        }

        public void RemoveFollowedUser(int userId, int followedUserId)
        {
            var followedUser = _context.FollowedUsers
                .FirstOrDefault(f => f.UserID == userId && f.FollowedUserID == followedUserId);

            if (followedUser != null)
            {
                _context.FollowedUsers.Remove(followedUser);
                _context.SaveChanges();
            }
        }
    }
}
