namespace AppCommonClasses.Repos
{
    using System.Collections.Generic;
    using System.Linq;
    using AppCommonClasses.Data;
    using AppCommonClasses.DbRelationshipEntities;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Group = Models.Group;

    /// <summary>
    /// Repository for managing groups.
    /// </summary>
    public class GroupRepository : IGroupRepository
    {
        private readonly SocialAppDbContext dbContext;

        public GroupRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Gets a group by ID from the Database.
        /// </summary>
        /// <param name="id">The ID of the group to retrieve.</param>
        /// <returns>The group with the specified ID.</returns>
        public Group GetGroupById(long id)
        {
            return dbContext.Groups.First(g => g.Id == id);
        }

        /// <summary>
        /// Gets all groups from the Database.
        /// </summary>
        /// <returns>Returns a list of all groups.</returns>
        public List<Group> GetAllGroups()
        {
            return dbContext.Groups.ToList();
        }

        /// <summary>
        /// Gets groups that a user is a member of.
        /// </summary>
        /// <param name="userId">The ID of the user whose groups to retrieve.</param>
        /// <returns>A list of groups the user belongs to.</returns>
        public List<Group> GetGroupsForUser(long userId)
        {
            var groupsQuery = from groups in dbContext.Groups
                              join gud in dbContext.GroupUsers
                              on groups.Id equals gud.GroupId
                              where gud.UserId == userId
                              select groups;

            return groupsQuery.ToList();
        }


        /// <summary>
        /// Gets all users in a group from the Database.
        /// </summary>
        /// <param name="groupId">The ID of the group.</param>
        /// <returns>A list of users in the group.</returns>
        public List<User> GetUsersFromGroup(long groupId)
        {
            var usersQuery = from user in dbContext.Users
                             join groupUser in dbContext.GroupUsers
                             on user.Id equals groupUser.UserId
                             where groupUser.GroupId == groupId
                             select user;

            return usersQuery.ToList();
        }

        /// <summary>
        /// Adds a new group in the Database.
        /// </summary>
        /// <param name="entity">The group that needs to be added.</param>
        public void SaveGroup(Group entity)
        {
            dbContext.Groups.Add(entity);
            dbContext.SaveChanges();

            dbContext.GroupUsers.Add(new GroupUser
            {
                GroupId = entity.Id,
                UserId = entity.AdminId
            });

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates a group by ID from the Database.
        /// </summary>
        /// <param name="id">The ID of the group to update.</param>
        /// <param name="name">The new name of the group.</param>
        /// <param name="image">The new image of the group.</param>
        /// <param name="description">The new description of the group.</param>
        /// <param name="adminId">The new admin ID of the group.</param>
        public void UpdateGroup(long id, string name, string image, string description, long adminId)
        {
            var group = dbContext.Groups.Find(id);
            if (group != null)
            {
                group.Name = name;
                group.Image = image;
                group.Description = description;
                group.AdminId = adminId;
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a group by ID from the Database.
        /// </summary>
        /// <param name="id">The ID of the group to delete.</param>
        public void DeleteGroupById(long id)
        {
            var group = dbContext.Groups.Find(id);
            if (group != null)
            {
                dbContext.Groups.Remove(group);
                dbContext.SaveChanges();
            }
        }
    }
}
