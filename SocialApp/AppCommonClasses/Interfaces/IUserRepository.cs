using AppCommonClasses.Models;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace AppCommonClasses.Interfaces
{
    public interface IUserRepository
    {
        void DeleteById(long id);
        void Follow(long userId, long whoToFollowId);
        List<User> GetAll();
        User GetByEmail(string email);
        User GetById(long id);
        User GetByUsername(string username);
        List<User> GetUserFollowers(long id);
        List<User> GetUserFollowing(long id);
        void Save(User entity);

        void Unfollow(long userId, long whoToUnfollowId);
        void UpdateById(long id, string username, string email, string hashPassword, string image);
    }
}