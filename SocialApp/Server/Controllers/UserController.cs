namespace Server.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AppCommonClasses.DTOs;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    using Server.Interfaces;
    using Server.Repos;
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(long userId)
        {
            try
            {
                this.userRepository.DeleteById(userId);
                return this.Ok();
            }
            catch
            {
                Debug.WriteLine("Error deleting user");
                return this.BadRequest();

            }
        }

        [HttpPost("users/{userId}/followers")]
        public IActionResult FollowUser(long userId, long followUserId)
        {
            this.userRepository.Follow(userId, followUserId);
            return this.Ok();
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return this.userRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(long id)
        {
            try
            {
                return this.userRepository.GetById(id);
            }catch
            {
                return this.BadRequest();
            }
        }

        [HttpGet("users/{username}")]
        public ActionResult<User> GetUserByUsername(string username)
        {
            try
            {
                return this.userRepository.GetByUsername(username);
            }
            catch
            {
                return this.BadRequest();
            }
        }

        [HttpGet("users/{userId}/followers")]
        public ActionResult<List<User>> GetUserFollowers(long userId)
        {
            return this.userRepository.GetUserFollowers(userId);
        }

        [HttpGet("users/{userId}/following")]
        public ActionResult<List<User>> GetUserFollowing(long userId)
        {
            return this.userRepository.GetUserFollowing(userId);
        }

        [HttpPost]
        public IActionResult SaveUser(User user)
        {
            this.userRepository.Save(user);
            return this.Ok();
        }

        [HttpDelete("users/{userId}/followers/{unfollowUserId}")]
        public IActionResult UnfollowUser(long userId, long unfollowUserId)
        {
            this.userRepository.Unfollow(userId, unfollowUserId);
            return this.Ok();
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(long userId, [FromBody] UserModelDTO user)
        {
            var existingUser = this.userRepository.GetById(userId);
            if (existingUser == null)
            {
                return this.NotFound();
            }
            string name = user.Name;
            string email = user.Email;
            string password = user.HashPassword;
            string image = user.Image;
            try {
                this.userRepository.UpdateById(userId, name, email, password, image);
                return this.Ok(existingUser);
            }
            catch
            {
                Debug.WriteLine("Error updating user");
                return this.BadRequest();
            }
            
        }
    }
}
