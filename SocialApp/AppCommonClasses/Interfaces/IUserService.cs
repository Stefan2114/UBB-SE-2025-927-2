using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface IUserService
    {
        void FollowUserById(long userId, long whoToFollowId);

        List<User> GetAllUsers();

        User GetById(long id);

        List<User> GetUserFollowers(long id);

        List<User> GetUserFollowing(long id);

        User? GetUserByUsername(string username);

        User Save(User entity);

        List<User> SearchUsersById(long userId, string query);

        void UnfollowUserById(long userId, long whoToUnfollowId);

        long AddUser(string username, string email, string password, string image);

        void DeleteUser(long id);

        void UpdateUser(long id, string username, string email, string password, string? image);

        long Login(string username, string password);
    }
}