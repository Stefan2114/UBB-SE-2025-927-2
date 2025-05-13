using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;

namespace Server.Interfaces
{
    public interface IPostController
    {
        IActionResult DeletePost(long postId);
        ActionResult<List<Post>> FetHomeFeed(long userId);
        ActionResult<List<Post>> GetAllPosts();
        ActionResult<List<Post>> GetGroupFeed(long userId);
        ActionResult<Post> GetPostById(long id);
        ActionResult<List<Post>> GetPostsByGroupId(long groupId);
        ActionResult<List<Post>> GetPostsByUserId(long userId);
        IActionResult SavePost(Post post);
        IActionResult UpdatePost(long postId, [FromBody] PostDTO post);
    }
}