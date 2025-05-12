using System.Collections.Generic;
using AppCommonClasses.Models;

namespace SocialApp.Interfaces
{
    public interface IUserService
    {
        void FollowUserById(long userId, long whoToFollowId);

        List<UserModel> GetAllUsers();

        UserModel GetById(long id);

        List<UserModel> GetUserFollowers(long id);

        List<UserModel> GetUserFollowing(long id);

        List<UserModel> SearchUsersById(long userId, string query);

        void UnfollowUserById(long userId, long whoToUnfollowId);

        void AddUser(string username, string email, string password, string image);

        void DeleteUser(long id);

        void UpdateUser(long id, string username, string email, string password, string? image);
    }
}