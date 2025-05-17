using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialApp.Controllers
{
    public class MealController : Controller
    {
        private readonly MealServiceProxy mealServiceProxy;

        public MealController(MealServiceProxy mealServiceProxy)
        {
            this.mealServiceProxy = mealServiceProxy;
        }

        // GET: /Meal
        public async Task<IActionResult> Index()
        {
            var meals = await mealServiceProxy.RetrieveAllMealsAsync();
            return View(meals);
        }

        // GET: /Meal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Meal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meal meal)
        {
            if (ModelState.IsValid)
            {
                var result = await mealServiceProxy.CreateMealAsync(meal);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Failed to create meal.");
            }
            return View(meal);
        }
    }
}
