namespace AppCommonClasses.Repos
{
    using System.Collections.Generic;
    using System.Linq;
    using AppCommonClasses.Data;
    using AppCommonClasses.DbRelationshipEntities;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;

    /// <summary>
    /// Repository for managing user-related operations in the database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly SocialAppDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used.</param>
        public UserRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Deletes a user by ID from the database.
        /// </summary>
        /// <param name="id">The id of the user that has to be deleted</param>
        public void DeleteById(long id)
        {
            User? user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
        }

        public void Follow(long userId, long whoToFollowId)
        {
            dbContext.UserFollowers.Add(new UserFollower
            {
                UserId = userId,
                FollowerId = whoToFollowId,
            });
            dbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return dbContext.Users.ToList();
        }


        public User GetById(long id)
        {
            return dbContext.Users.First(u => u.Id == id);
        }

        public User? GetByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetUserFollowers(long id)

        {
            List<User> userFollowers = new List<User>();
            List<UserFollower> followers = dbContext.UserFollowers
                .Where(uf => uf.UserId == id)
                .ToList();
            foreach (UserFollower userFollower in followers)
            {
                User? user = dbContext.Users.FirstOrDefault(u => u.Id == userFollower.FollowerId);
                if (user != null)
                {
                    userFollowers.Add(user);
                }
            }

            return userFollowers;
        }

        public List<User> GetUserFollowing(long id)
        {
            List<User> userFollowing = new List<User>();
            List<UserFollower> following = dbContext.UserFollowers
                .Where(uf => uf.FollowerId == id)
                .ToList();
            foreach (UserFollower userFollower in following)
            {
                User? user = dbContext.Users.FirstOrDefault(u => u.Id == userFollower.UserId);
                if (user != null)
                {
                    userFollowing.Add(user);
                }
            }
            return userFollowing;
        }

        public User Save(User entity)
        {
            if (dbContext.Users.FirstOrDefault(u => u.Username.Equals(entity.Username)) != null)
            {
                throw new Exception("User already exists");
            }

            dbContext.Users.Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public void Unfollow(long userId, long whoToUnfollowId)
        {
            UserFollower? userFollower = dbContext.UserFollowers
                .FirstOrDefault(uf => uf.UserId == userId && uf.FollowerId == whoToUnfollowId);
            if (userFollower != null)
            {
                dbContext.UserFollowers.Remove(userFollower);
                dbContext.SaveChanges();
            }
        }

        public void UpdateById(long id, string username, string email, string hashPassword, string image)
        {
            User? user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Username = username;
                user.Password = hashPassword;
                dbContext.SaveChanges();
            }
        }
    }
}