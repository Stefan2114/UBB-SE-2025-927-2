using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

namespace AppCommonClasses.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;
        private readonly IUserRepository userRepository;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
        }

        public Group GetGroupById(long id)
        {
            return groupRepository.GetGroupById(id);
        }

        public List<Group> GetGroups(long userId)
        {
            return groupRepository.GetGroupsForUser(userId);
        }

        public List<User> GetUsersFromGroup(long groupId)
        {
            return groupRepository.GetUsersFromGroup(groupId);
        }

        public Group AddGroup(string name, string desc, string image, long adminId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Group name cannot be empty");
            }

            if (userRepository.GetById(adminId) == null)
            {
                throw new ArgumentException("User does not exist");
            }

            var group = new Group
            {
                Name = name,
                Description = desc,
                Image = image,
                AdminId = adminId
            };

            groupRepository.SaveGroup(group);
            return group;
        }

        public void DeleteGroup(long groupId)
        {
            if (groupRepository.GetGroupById(groupId) == null)
            {
                throw new ArgumentException("Group does not exist");
            }

            groupRepository.DeleteGroupById(groupId);
        }

        public void UpdateGroup(long id, string name, string desc, string image, long adminId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Group name cannot be empty");
            }

            var group = groupRepository.GetGroupById(id);
            if (group == null)
            {
                throw new ArgumentException($"Group with ID {id} does not exist");
            }

            if (userRepository.GetById(adminId) == null)
            {
                throw new ArgumentException($"User with ID {adminId} does not exist");
            }

            try
            {
                groupRepository.UpdateGroup(id, name, image, desc, adminId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update group: {ex.Message}");
            }
        }

        public List<Group> GetAllGroups()
        {
            return groupRepository.GetAllGroups();
        }
    }
} 