using Microsoft.AspNetCore.Mvc;
using AppCommonClasses.Models;
using AppCommonClasses.Interfaces;

namespace MealSocialServerMVC.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        // GET: /Meal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Meal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Index()
        {
            var meals = await mealService.RetrieveAllMealsAsync();
            return View(meals);
        }
    }
}
