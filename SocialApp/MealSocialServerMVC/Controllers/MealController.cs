using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    [ApiController]
    [Route("meals")]
    public class MealController : Controller
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        // GET: /Meal/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Meal/Create
        [HttpPost("create")]
        public async Task<IActionResult> Create(Meal model)
        {
            if (ModelState.IsValid)
            {
                bool created = await mealService.CreateMealWithCookingLevelAsync(model, model.CookingLevel);

                if (created)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Failed to create meal.");
            }
            return View(model);
        }

        // GET: /Meal/Index
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var meals = await mealService.RetrieveAllMealsAsync();
            return View(meals);
        }
    }
}
