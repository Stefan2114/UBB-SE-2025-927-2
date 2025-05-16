namespace Server.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    using Server.DTOs;
    using Server.Interfaces;
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(long userId)
        {
            try
            {
                this.userService.DeleteUser(userId);
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
            this.userService.FollowUserById(userId, followUserId);
            return this.Ok();
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return this.userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(long id)
        {
            try
            {
                return this.userService.GetById(id);
            }
            catch
            {
                return this.BadRequest();
            }
        }

        [HttpGet("users/{username}")]
        public ActionResult<User> GetUserByUsername(string username)
        {
            try
            {
                return this.userService.GetUserByUsername(username);
            }
            catch
            {
                return this.BadRequest();
            }
        }

        [HttpGet("users/{userId}/followers")]
        public ActionResult<List<User>> GetUserFollowers(long userId)
        {
            return this.userService.GetUserFollowers(userId);
        }

        [HttpGet("users/{userId}/following")]
        public ActionResult<List<User>> GetUserFollowing(long userId)
        {
            return this.userService.GetUserFollowing(userId);
        }

        [HttpPost]
        public IActionResult SaveUser(User user)
        {
            var savedUser = this.userService.Save(user);
            return this.Ok(savedUser);
        }

        [HttpDelete("users/{userId}/followers/{unfollowUserId}")]
        public IActionResult UnfollowUser(long userId, long unfollowUserId)
        {
            this.userService.UnfollowUserById(userId, unfollowUserId);
            return this.Ok();
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(long userId, [FromBody] UserModelDTO user)
        {
            var existingUser = this.userService.GetById(userId);
            if (existingUser == null)
            {
                return this.NotFound();
            }
            string name = user.Name;
            string email = user.Email;
            string password = user.HashPassword;
            string image = user.Image;
            try
            {
                this.userService.UpdateUser(userId, name, email, password, image);
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
