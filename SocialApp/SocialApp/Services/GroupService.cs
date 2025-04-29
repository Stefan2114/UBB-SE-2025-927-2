namespace SocialApp.Services
{

    using System;
    using System.Collections.Generic;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using SocialApp.Interfaces;
    using SocialApp.Repository;

    public class GroupService : IGroupService
    {
        private IGroupRepository groupRepository;
        private IUserRepository userRepository;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
        }

        public Group AddGroup(string name, string desc, string image, long adminId)
        {
            if (name == null || name.Length == 0)
            {
                throw new Exception("Group name cannot be empty");
            }

            if (userRepository.GetById(adminId) == null)
            {
                throw new Exception("User does not exist");
            }

            Group group = new Group() { Name = name, AdminId = adminId, Image = image, Description = desc };

            groupRepository.SaveGroup(group);
            return group;
        }

        public void DeleteGroup(long groupId)
        {
            if (this.groupRepository.GetGroupById(groupId) == null)
                throw new Exception("Group does not exist");

            this.groupRepository.DeleteGroupById(groupId);
        }

        public void UpdateGroup(long id, string name, string desc, string image, long adminId)
        {
            if (this.groupRepository.GetGroupById(id) == null)
            {
                throw new Exception("Group does not exist");
            }

            if (this.userRepository.GetById(adminId) == null)
            {
                throw new Exception("User does not exist");
            }

            if (name == null || name.Length == 0)
            {
                throw new Exception("Group name cannot be empty");
            }

            this.groupRepository.UpdateGroup(id, name, image, desc, adminId);
        }

        public List<Group> GetAll()
        {
            return this.groupRepository.GetAllGroups();
        }

        public Group GetGroupById(long id)
        {
            return this.groupRepository.GetGroupById(id);
        }

        public List<User> GetUsersFromGroup(long groupId)
        {
            return this.groupRepository.GetUsersFromGroup(groupId);
        }

        public List<Group> GetGroups(long userId)
        {
            return this.groupRepository.GetGroupsForUser(userId);
        }
    }
}