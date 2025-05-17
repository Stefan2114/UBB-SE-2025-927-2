using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MealSocialServerMVC.Models;
using System;
using System.Diagnostics;

namespace MealSocialServerMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICalorieService _calorieService;
        private readonly IUserService _userService;

        public DashboardController(ICalorieService calorieService, IUserService userService)
        {
            _calorieService = calorieService;
            _userService = userService;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard(string username = "andu")
        {
            try
            {
                // Get user by username
                var user = _userService.GetUserByUsername(username);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User '{username}' not found. Using default values.";
                    return View(new CaloriesViewModel
                    {
                        Username = username,
                        DailyGoal = 2000,
                        CaloriesConsumed = 0,
                        CaloriesBurned = 0
                    });
                }

                // Get actual calorie data for the user
                var viewModel = new CaloriesViewModel
                {
                    Username = username,
                    DailyGoal = _calorieService.GetGoal(user.Id),
                    CaloriesConsumed = _calorieService.GetFood(user.Id),
                    CaloriesBurned = _calorieService.GetExercise(user.Id)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Dashboard: {ex.Message}");
                ViewBag.ErrorMessage = $"Error retrieving user data: {ex.Message}";

                return View(new CaloriesViewModel
                {
                    Username = username,
                    DailyGoal = 2000,
                    CaloriesConsumed = 0,
                    CaloriesBurned = 0
                });
            }
        }
    }
}
