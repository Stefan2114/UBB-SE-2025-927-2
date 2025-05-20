using AppCommonClasses.Models;
using MealSocialServerMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    [Route("comments")]
    public class CreateCommentController : Controller
    {
        private readonly HttpClient httpClient;

        public CreateCommentController()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7106/comments/")
            };
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentViewModel model)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            long userId = long.Parse(userIdString);
            if (!ModelState.IsValid)
                return View(model);

            Comment newComment = new Comment
            {
                Content = model.Content,
                UserId = userId,
                PostId = model.PostId,
                CreatedDate = DateTime.UtcNow
            };

            var response = await httpClient.PostAsJsonAsync("", newComment);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Comment created successfully!";
                ModelState.Clear();
                return View(new CreateCommentViewModel());
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
