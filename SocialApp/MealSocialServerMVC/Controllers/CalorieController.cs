using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MealSocialServerMVC.Models;
using System;
using System.Diagnostics;

namespace MealSocialServerMVC.Controllers
{
    public class CalorieController : Controller
    {
        private readonly ICalorieService _calorieService;
        private readonly IUserService _userService;

        public CalorieController(ICalorieService calorieService, IUserService userService)
        {
            _calorieService = calorieService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Dashboard(string username = "demo")
        {
            try
            {
                var user = _userService.GetUserByUsername(username);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User '{username}' not found. Using default values.";

                    // Return a model with default values
                    return View(new CaloriesViewModel
                    {
                        Username = username,
                        DailyGoal = 2000,
                        CaloriesConsumed = 800,
                        CaloriesBurned = 200
                    });
                }

                // Get real values from service
                var caloriesViewModel = new CaloriesViewModel
                {
                    Username = username,
                    DailyGoal = _calorieService.GetGoal(user.Id),
                    CaloriesConsumed = _calorieService.GetFood(user.Id),
                    CaloriesBurned = _calorieService.GetExercise(user.Id)
                };

                return View(caloriesViewModel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Calorie Dashboard: {ex.Message}");
                ViewBag.ErrorMessage = $"Error retrieving calorie data: {ex.Message}";

                return View(new CaloriesViewModel { Username = username });
            }
        }

        [HttpGet]
        public IActionResult Widget(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return PartialView("_CalorieWidget", new CaloriesViewModel());
            }

            try
            {
                var user = _userService.GetUserByUsername(username);
                if (user == null)
                {
                    return PartialView("_CalorieWidget", new CaloriesViewModel { Username = username });
                }

                var viewModel = new CaloriesViewModel
                {
                    Username = username,
                    DailyGoal = _calorieService.GetGoal(user.Id),
                    CaloriesConsumed = _calorieService.GetFood(user.Id),
                    CaloriesBurned = _calorieService.GetExercise(user.Id)
                };

                return PartialView("_CalorieWidget", viewModel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving calorie widget: {ex.Message}");
                return PartialView("_CalorieWidget", new CaloriesViewModel { Username = username });
            }
        }

        // This will be implemented later when you add meal tracking functionality
        [HttpPost]
        public IActionResult AddMeal(string username, string mealType, double calories)
        {
            // Implementation placeholder
            return RedirectToAction("Dashboard", new { username });
        }
    }
}

