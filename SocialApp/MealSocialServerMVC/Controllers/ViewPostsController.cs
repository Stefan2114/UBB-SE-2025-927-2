using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using MealSocialServerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http;
using System.Text;
using System.Text.Json;

//localhost/posts
namespace MealSocialServerMVC.Controllers
{

    [Route ("posts")]
    public class ViewPostsController : Controller
    {
        private readonly IPostService postService;

        public ViewPostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Post> posts = postService.GetAllPosts(); // You should have this in your IPostService
            return View(posts);
        }
    }
}
