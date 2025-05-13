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
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        /// <summary>
        /// Retrieves all comments.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Comment>> GetAllComments()
        {
            var comments = commentRepository.GetAllComments();
            return Ok(comments);
        }

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        [HttpGet("{id:long}")]
        public ActionResult<Comment> GetCommentById(long id)
        {
            var comment = commentRepository.GetCommentById(id);
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
            var comments = commentRepository.GetCommentsByPostId(postId);
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

            commentRepository.SaveComment(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
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

            var existingComment = commentRepository.GetCommentById(commentId);
            if (existingComment == null)
            {
                return NotFound($"Comment with ID {commentId} not found.");
            }

            existingComment.Content = commentDto.Content;
            commentRepository.UpdateCommentContentById(commentId, commentDto.Content);

            return NoContent();
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        [HttpDelete("{commentId:long}")]
        public IActionResult DeleteComment(long commentId)
        {
            var existingComment = commentRepository.GetCommentById(commentId);
            if (existingComment == null)
            {
                return NotFound($"Comment with ID {commentId} not found.");
            }

            commentRepository.DeleteCommentById(commentId);
            return NoContent();
        }
    }
}
