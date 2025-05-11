namespace SocialApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using SocialApp.Interfaces;
    using SocialApp.Repository;

    /// <summary>
    /// Provides user-related services.
    /// </summary>

    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Validates and adds a new user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="image">The image.</param>
        /// <exception cref="Exception">Thrown when validation fails.</exception>
        public void AddUser(string username, string email, string password, string image)
        {
            if (username == null || username.Length == 0)
            {
                throw new Exception("Username cannot be empty");
            }

            if (email == null || email.Length == 0)
            {
                throw new Exception("Email cannot be empty");
            }

            if (password == null || password.Length == 0)
            {
                throw new Exception("Password cannot be empty");
            }

            this.userRepository.Save(new UserModel() { Name = username, Email = email});
        }

        /// <summary>
        /// Validates and deletes a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <exception cref="Exception">Thrown when the user does not exist.</exception>
        public void DeleteUser(long id)
        {
            if (this.userRepository.GetById(id) == null)
            {
                throw new Exception("User does not exist");
            }

            this.userRepository.DeleteById(id);
        }

        /// <summary>
        /// Validates and updates a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="username">The username.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="image">The image.</param>
        /// <exception cref="Exception">Thrown when the user does not exist.</exception>
        public void UpdateUser(long id, string username, string email, string password, string? image)
        {
            if (this.userRepository.GetById(id) == null)
            {
                throw new Exception("User does not exist");
            }

            this.userRepository.UpdateById(id, username, email);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        public List<UserModel> GetAllUsers()
        {
            return this.userRepository.GetAll();
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user.</returns>
        public UserModel GetById(long id)
        {
            return this.userRepository.GetById(id);
        }

        /// <summary>
        /// Gets the followers of a user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A list of followers.</returns>
        public List<UserModel> GetUserFollowers(long id)
        {
            return this.userRepository.GetUserFollowers(id);
        }

        /// <summary>
        /// Gets the users followed by a user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A list of followed users.</returns>
        public List<UserModel> GetUserFollowing(long id)
        {
            return this.userRepository.GetUserFollowing(id);
        }

        /// <summary>
        /// Follows a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="whoToFollowId">The ID of the user to follow.</param>
        /// <exception cref="Exception">Thrown when the user or the user to follow does not exist.</exception>
        public void FollowUserById(long userId, long whoToFollowId)
        {
            if (this.userRepository.GetById(userId) == null)
            {
                throw new Exception("User does not exist");
            }

            if (this.userRepository.GetById(whoToFollowId) == null)
            {
                throw new Exception("User to follow does not exist");
            }

            this.userRepository.Follow(userId, whoToFollowId);
        }

        /// <summary>
        /// Unfollows a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="whoToUnfollowId">The ID of the user to unfollow.</param>
        /// <exception cref="Exception">Thrown when the user or the user to unfollow does not exist.</exception>
        public void UnfollowUserById(long userId, long whoToUnfollowId)
        {
            if (this.userRepository.GetById(userId) == null)
            {
                throw new Exception("User does not exist");
            }

            if (this.userRepository.GetById(whoToUnfollowId) == null)
            {
                throw new Exception("User to unfollow does not exist");
            }

            this.userRepository.Unfollow(userId, whoToUnfollowId);
        }

        /// <summary>
        /// Searches for users by query.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="query">The search query.</param>
        /// <returns>A list of users matching the query.</returns>
        public List<UserModel> SearchUsersById(long userId, string query)
        {
            var followingUsers = this.GetUserFollowing(userId);
            return followingUsers.Where(u => u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
