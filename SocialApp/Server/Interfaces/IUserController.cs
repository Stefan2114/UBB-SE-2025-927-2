namespace Server.Interfaces
{
    using AppCommonClasses.DTOs;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    public interface IUserController
    {
        IActionResult DeleteUser(long userId);
        ActionResult<List<User>> GetAllUsers();
        ActionResult<User> GetUserById(long id);
        ActionResult<User> GetUserByUsername(string username);
        IActionResult SaveUser(User user);
        IActionResult UpdateUser(long userId, [FromBody] UserModelDTO user);
        IActionResult FollowUser(long userId, long followUserId);
        IActionResult UnfollowUser(long userId, long unfollowUserId);
        ActionResult<List<User>> GetUserFollowers(long userId);
        ActionResult<List<User>> GetUserFollowing(long userId);
    }
}
