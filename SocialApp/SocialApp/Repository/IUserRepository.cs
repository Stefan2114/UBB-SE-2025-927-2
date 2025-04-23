using SocialApp.Entities;
using System.Collections.Generic;

namespace SocialApp.Repository
{
    public interface IUserRepository
    {
        void DeleteById(long id);
        void Follow(long userId, long whoToFollowId);
        List<User> GetAll();
        User GetByEmail(string email);
        User GetById(long id);
        List<User> GetUserFollowers(long id);
        List<User> GetUserFollowing(long id);
        void Save(User entity);
        void Unfollow(long userId, long whoToUnfollowId);
        void UpdateById(long id, string username, string email, string passwordHash, string? image);
    }
}