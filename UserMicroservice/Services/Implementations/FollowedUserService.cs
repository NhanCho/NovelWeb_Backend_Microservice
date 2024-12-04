using UserMicroservice.Models;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Services.Implementations
{
    public class FollowedUserService : IFollowedUserService
    {
        private readonly IFollowedUserRepository _followedUserRepository;

        public FollowedUserService(IFollowedUserRepository followedUserRepository)
        {
            _followedUserRepository = followedUserRepository;
        }

        public IEnumerable<FollowedUser> GetFollowedUsersByUserId(int userId)
        {
            return _followedUserRepository.GetFollowedUsersByUserId(userId);
        }

        public void AddFollowedUser(FollowedUser followedUser)
        {
            _followedUserRepository.AddFollowedUser(followedUser);
        }

        public void RemoveFollowedUser(int userId, int followedUserId)
        {
            _followedUserRepository.RemoveFollowedUser(userId, followedUserId);
        }
    }
}
