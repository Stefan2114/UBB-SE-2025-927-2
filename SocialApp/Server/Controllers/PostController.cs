using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;
using System;
using System.Collections.Generic;

namespace Server.Controllers
{
    /// <summary>
    /// Controller for managing post-related API endpoints.
    /// </summary>
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase, IPostController
    {
        private readonly IPostRepository postRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        /// <summary>
        /// Gets all posts.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Post>> GetAllPosts()
        {
            try
            {
                return Ok(this.postRepository.GetAllPosts());
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving posts: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a post by its ID.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Post> GetPostById(long id)
        {
            try
            {
                var post = this.postRepository.GetPostById(id);
                if (post == null)
                    return NotFound($"Post with ID {id} not found.");

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving post: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets posts created by a specific user.
        /// </summary>
        [HttpGet("user/{userId}")]
        public ActionResult<List<Post>> GetPostsByUserId(long userId)
        {
            try
            {
                return Ok(this.postRepository.GetPostsByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving user's posts: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets posts from a specific group.
        /// </summary>
        [HttpGet("group/{groupId}")]
        public ActionResult<List<Post>> GetPostsByGroupId(long groupId)
        {
            try
            {
                return Ok(this.postRepository.GetPostsByGroupId(groupId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving group's posts: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a user's home feed.
        /// </summary>
        [HttpGet("homefeed/{userId}")]
        public ActionResult<List<Post>> FetHomeFeed(long userId)
        {
            try
            {
                return Ok(this.postRepository.GetPostsHomeFeed(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving home feed: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets posts from groups that a user is part of.
        /// </summary>
        [HttpGet("groupfeed/{userId}")]
        public ActionResult<List<Post>> GetGroupFeed(long userId)
        {
            try
            {
                return Ok(this.postRepository.GetPostsGroupsFeed(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error retrieving group feed: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves a new post.
        /// </summary>
        [HttpPost]
        public IActionResult SavePost(Post post)
        {
            try
            {
                if (post == null)
                    return BadRequest("Post data cannot be null.");

                this.postRepository.SavePost(post);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error saving post: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a post by its ID.
        /// </summary>
        [HttpPut("{postId}")]
        public IActionResult UpdatePost(long postId, [FromBody] PostDTO post)
        {
            try
            {
                if (post == null)
                    return BadRequest("Post update data cannot be null.");

                bool updated = this.postRepository.UpdatePostById(postId, post.Title, post.Content, post.Visibility, post.Tag);
                if (!updated)
                    return NotFound($"Post with ID {postId} not found.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating post: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a post by its ID.
        /// </summary>
        [HttpDelete("{postId}")]
        public IActionResult DeletePost(long postId)
        {
            try
            {
                bool deleted = this.postRepository.DeletePostById(postId);
                if (!deleted)
                    return NotFound($"Post with ID {postId} not found.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error deleting post: {ex.Message}");
            }
        }
    }
}
