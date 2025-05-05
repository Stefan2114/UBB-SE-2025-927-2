namespace Server.Interfaces
{
    using AppCommonClasses.DTOs;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    public interface IUserController
    {
        IActionResult DeleteUser(long userId);
        ActionResult<List<UserModel>> GetAllUsers();
        ActionResult<UserModel> GetUserById(long id);
        ActionResult<UserModel> GetUserByUsername(string username);
        IActionResult SaveUser(UserModel user);
        IActionResult UpdateUser(long userId, [FromBody] UserModelDTO user);
        IActionResult FollowUser(long userId, long followUserId);
        IActionResult UnfollowUser(long userId, long unfollowUserId);
        ActionResult<List<UserModel>> GetUserFollowers(long userId);
        ActionResult<List<UserModel>> GetUserFollowing(long userId);
    }
}
