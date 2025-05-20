using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using MealSocialServerMVC.Models;
using Microsoft.AspNetCore.Mvc;

// localhost/posts/create
namespace MealSocialServerMVC.Controllers
{
    [Route("posts")]
    public class CreatePostController : Controller
    {
        private readonly IPostService postService;

        public CreatePostController(IPostService postService)
        {
            this.postService = postService;
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Post newPost = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Visibility = model.Visibility,
                Tag = model.Tag,
                UserId = 1,      // hardcoded
                GroupId = 0,    // hardcoded
                CreatedDate = DateTime.UtcNow
            };
            try
            {
                this.postService.SavePost(newPost);
                ViewBag.Message = "Post created successfully!";
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error creating post: {ex.Message}";
                return View(model);
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
