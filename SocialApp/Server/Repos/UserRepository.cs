namespace Server.Repos
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;
    using Server.Data;
    using Server.DbRelationshipEntities;

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
            this.dbContext = context;
        }

        /// <summary>
        /// Deletes a user by ID from the database.
        /// </summary>
        /// <param name="id">The id of the user that has to be deleted</param>
        public void DeleteById(long id)
        {
            UserModel? user = this.dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                this.dbContext.Users.Remove(user);
                this.dbContext.SaveChanges();
            }
        }

        public void Follow(long userId, long whoToFollowId)
        {
            this.dbContext.UserFollowers.Add(new UserFollower
            {
                UserId = userId,
                FollowerId = whoToFollowId,
            });
            this.dbContext.SaveChanges();
        }

        public List<UserModel> GetAll()
        {
            return this.dbContext.Users.ToList();
        }

        public UserModel GetByEmail(string email)
        {
            return this.dbContext.Users.First(u => u.Email == email);
        }

        public UserModel GetById(long id)
        {
            return this.dbContext.Users.First(u => u.Id == id);
        }

        public List<UserModel> GetUserFollowers(long id)
        {
            List<UserModel> userFollowers = new List<UserModel>();
            List<UserFollower> followers = this.dbContext.UserFollowers
                .Where(uf => uf.UserId == id)
                .Include(uf => uf.FollowerId)
                .ToList();
            foreach (UserFollower userFollower in followers)
            {
                UserModel? user = this.dbContext.Users.FirstOrDefault(u => u.Id == userFollower.FollowerId);
                if (user != null)
                {
                    userFollowers.Add(user);
                }
            }

            return userFollowers;
        }

        public List<UserModel> GetUserFollowing(long id)
        {
            List<UserModel> userFollowing = new List<UserModel>();
            List<UserFollower> following = this.dbContext.UserFollowers
                .Where(uf => uf.FollowerId == id)
                .Include(uf => uf.UserId)
                .ToList();
            foreach (UserFollower userFollower in following)
            {
                UserModel? user = this.dbContext.Users.FirstOrDefault(u => u.Id == userFollower.UserId);
                if (user != null)
                {
                    userFollowing.Add(user);
                }
            }
            return userFollowing;
        }

        public void Save(UserModel entity)
        {
            this.dbContext.Users.Add(entity);
            this.dbContext.SaveChanges();
        }

        public void Unfollow(long userId, long whoToUnfollowId)
        {
            UserFollower? userFollower = this.dbContext.UserFollowers
                .FirstOrDefault(uf => uf.UserId == userId && uf.FollowerId == whoToUnfollowId);
            if (userFollower != null)
            {
                this.dbContext.UserFollowers.Remove(userFollower);
                this.dbContext.SaveChanges();
            }
        }

        public void UpdateById(long id, string username, string email)
        {
            UserModel? user = this.dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Name = username;
                user.Email = email;
                this.dbContext.SaveChanges();
            }
        }
    }
}