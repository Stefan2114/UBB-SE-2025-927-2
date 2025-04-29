using AppCommonClasses.Models;
using System.Collections.Generic;

namespace SocialApp.Interfaces
{
    public interface IGroupService
    {
        Group GetGroupById(long id);

        List<Group> GetGroups(long userId);
        List<User> GetUsersFromGroup(long groupId);
        Group AddGroup(string name, string desc, string image, long adminId);
        void DeleteGroup(long groupId);
        void UpdateGroup(long id, string name, string desc, string image, long adminId);
    }
}