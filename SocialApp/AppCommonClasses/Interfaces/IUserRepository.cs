using AppCommonClasses.Models;
using System.Collections.Generic;

namespace AppCommonClasses.Interfaces
{
    public interface IUserRepository
    {
        void DeleteById(long id);
        void Follow(long userId, long whoToFollowId);
        List<UserModel> GetAll();
        UserModel GetByEmail(string email);
        UserModel GetById(long id);
        List<UserModel> GetUserFollowers(long id);
        List<UserModel> GetUserFollowing(long id);
        void Save(UserModel entity);
        void Unfollow(long userId, long whoToUnfollowId);
        void UpdateById(long id, string username, string email);
    }
}