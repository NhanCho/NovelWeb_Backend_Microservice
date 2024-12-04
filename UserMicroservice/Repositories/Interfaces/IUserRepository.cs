using UserMicroservice.Models;

namespace UserMicroservice.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
