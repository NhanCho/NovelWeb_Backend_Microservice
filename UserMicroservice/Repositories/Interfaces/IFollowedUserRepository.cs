using UserMicroservice.Models;

namespace UserMicroservice.Repositories.Interfaces
{
    public interface IFollowedUserRepository
    {
        IEnumerable<FollowedUser> GetFollowedUsersByUserId(int userId);
        void AddFollowedUser(FollowedUser followedUser);
        void RemoveFollowedUser(int userId, int followedUserId);
    }
}
