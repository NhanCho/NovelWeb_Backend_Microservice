using UserMicroservice.Models;

namespace UserMicroservice.Services.Interfaces
{
    public interface IFollowedUserService
    {
        IEnumerable<FollowedUser> GetFollowedUsersByUserId(int userId);
        void AddFollowedUser(FollowedUser followedUser);
        void RemoveFollowedUser(int userId, int followedUserId);
    }
}
