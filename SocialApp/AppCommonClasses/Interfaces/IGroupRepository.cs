using AppCommonClasses.Models;
using System.Collections.Generic;

namespace AppCommonClasses.Interfaces

{
    public interface IGroupRepository
    {
        void DeleteGroupById(long id);

        List<Group> GetAllGroups();

        Group GetGroupById(long id);

        List<Group> GetGroupsForUser(long userId);

        List<UserModel> GetUsersFromGroup(long id);

        void SaveGroup(Group entity);

        void UpdateGroup(long id, string name, string image, string description, long adminId);
    }
}