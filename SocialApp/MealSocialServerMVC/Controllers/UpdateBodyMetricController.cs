using System.Diagnostics;
using AppCommonClasses.Interfaces;
using MealSocialServerMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    [Route("bodymetrics")]
    public class UpdateBodyMetricController : Controller
    {
        private readonly IBodyMetricRepository bodyMetricRepository;
        private readonly IUserService userService;

        public UpdateBodyMetricController(IBodyMetricRepository bodyMetricRepository, IUserService userService)
        {
            this.bodyMetricRepository = bodyMetricRepository;
            this.userService = userService;
        }

        [Route("update")]
        [HttpGet]
        public IActionResult Update()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            long userId = long.Parse(userIdString);
            string username = userService.GetById(userId).Username;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            // Create the view model and populate it with the username
            var viewModel = new UpdateBodyMetricViewModel
            {
                Username = username
            };

            return View(viewModel);
        }

        [HttpPost("update")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateBodyMetricViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Find user by username
                var user = userService.GetUserByUsername(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError("", $"User '{model.Username}' not found");
                    return View(model);
                }

                // Update the user's body metrics
                bodyMetricRepository.UpdateUserBodyMetrics(
                    user.Id,
                    model.Weight,
                    model.Height,
                    model.TargetWeight);

                ViewBag.Message = "Body metrics updated successfully!";

                // After successful update, redirect to the goals page
                // return RedirectToAction("Update", new { username = model.Username });
                return RedirectToAction("Dashboard", "Dashboard");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating body metrics: {ex.Message}");
                ViewBag.Message = $"Error updating body metrics: {ex.Message}";
                return View(model);
            }
        }

        public IActionResult Success()
        {
            ViewBag.Message = "Body metrics updated successfully! (Redirect to Goal/Update will work when implemented)";
            return View(); // Stay on the same page with a message

        }
    }
}
