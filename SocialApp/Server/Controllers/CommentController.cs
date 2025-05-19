using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase, ICommentController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Retrieves all comments.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Comment>> GetAllComments()
        {
            var comments = _commentService.GetAllComments();
            return Ok(comments);
        }

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        [HttpGet("{id:long}")]
        public ActionResult<Comment> GetCommentById(long id)
        {
            var comment = _commentService.GetCommentById((int)id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }
            return Ok(comment);
        }

        /// <summary>
        /// Retrieves all comments for a specific post.
        /// </summary>
        [HttpGet("post/{postId:long}")]
        public ActionResult<List<Comment>> GetCommentsByPostId(long postId)
        {
            var comments = _commentService.GetCommentsByPostId(postId);
            return Ok(comments);
        }

        /// <summary>
        /// Saves a new comment.
        /// </summary>
        [HttpPost]
        public IActionResult SaveComment([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest("Comment cannot be null.");
            }

            var savedComment = _commentService.AddComment(comment.Content, comment.UserId, comment.PostId);
            return CreatedAtAction(nameof(GetCommentById), new { id = savedComment.Id }, savedComment);
        }

        /// <summary>
        /// Updates the content of an existing comment.
        /// </summary>
        [HttpPut("{commentId:long}")]
        public IActionResult UpdateComment(long commentId, [FromBody] CommentDTO commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest("Comment data cannot be null.");
            }

            var existingComment = _commentService.GetCommentById((int)commentId);
            if (existingComment == null)
            {
                return NotFound($"Comment with ID {commentId} not found.");
            }

            _commentService.UpdateComment(commentId, commentDto.Content);
            return NoContent();
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        [HttpDelete("{commentId:long}")]
        public IActionResult DeleteComment(long commentId)
        {
            var existingComment = _commentService.GetCommentById((int)commentId);
            if (existingComment == null)
            {
                return NotFound($"Comment with ID {commentId} not found.");
            }

            _commentService.DeleteComment(commentId);
            return NoContent();
        }
    }
}
