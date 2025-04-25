using AppCommonClasses.Models;
using AppCommonClasses.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{

    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        [HttpGet]
        public ActionResult<List<Post>> GetAllPosts()
        {
            return this.postRepository.GetAllPosts();
        }


        [HttpGet("{id}")]
        public ActionResult<Post> GetPostById(long id)
        {
            return this.postRepository.GetPostById(id);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Post>> GetPostsByUserId(long userId)
        {
            return this.postRepository.GetPostsByUserId(userId);
        }


        [HttpGet("group/{groupId}")]
        public ActionResult<List<Post>> GetPostsByGroupId(long groupId)
        {
            return this.postRepository.GetPostsByGroupId(groupId);
        }


        [HttpGet("homefeed/{userId}")]
        public ActionResult<List<Post>> FetHomeFeed(long userId)
        {
            System.Diagnostics.Debug.WriteLine("Fetching home feed for user: " + userId);
            return this.postRepository.GetPostsHomeFeed(userId);

        }

        [HttpGet("groupfeed/{userId}")]
        public ActionResult<List<Post>> GetGroupFeed(long userId)
        {
            return this.postRepository.GetPostsGroupsFeed(userId);
        }


        [HttpPost]
        public IActionResult SavePost(Post post)
        {
            this.postRepository.SavePost(post);
            return Ok();
        }

        [HttpPut("{postId}")]
        public IActionResult UpdatePost(long postId, [FromBody] PostDTO post)
        {
            this.postRepository.UpdatePostById(postId, post.Title, post.Content, post.Visibility, post.Tag);
            return Ok();
        }

        [HttpDelete("{postId}")]
        public IActionResult DeletePost(long postId)
        {
            this.postRepository.DeletePostById(postId);
            return Ok();
        }



    }
}
