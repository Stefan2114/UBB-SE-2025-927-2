using AppCommonClasses.Models;
using MealSocialServerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

// localhost/posts/create
namespace MealSocialServerMVC.Controllers
{
    [Route("posts")]
    public class CreatePostController : Controller
    {
        private readonly HttpClient httpClient;

        public CreatePostController()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7106/posts/")
            };
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
                UserId = 1,    // hardcoded
                GroupId = 0,    // hardcoded
                CreatedDate = DateTime.UtcNow
            };

            var response = await httpClient.PostAsJsonAsync("", newPost);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Post created successfully!";
                ModelState.Clear();
                return View(new CreatePostViewModel());
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"❌ Server error: {error}");
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
