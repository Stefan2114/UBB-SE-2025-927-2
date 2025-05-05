using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface ICommentController
    {
        IActionResult DeleteComment(long commentId);
        ActionResult<List<Comment>> GetAllComments();
        ActionResult<Comment> GetCommentById(long id);
        ActionResult<List<Comment>> GetCommentsByPostId(long postId);
        IActionResult SaveComment(Comment comment);
        IActionResult UpdateComment(long commentId, [FromBody] CommentDTO comment);
    }
}