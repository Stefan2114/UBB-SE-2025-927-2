using System.Diagnostics;
using AppCommonClasses.Interfaces;
using MealSocialServerMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    [ApiController]
    [Route("UserController")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm] AuthenticationModel user)
        {
            try
            {
                long result = this._userService.AddUser(user.Username, " ", user.Password, "");

                HttpContext.Session.SetString("UserId", result.ToString());
                // at this point, the register is successful
                // here you redirect to Body Metrics page (for registering new user)

                return RedirectToAction("Update", "UpdateBodyMetric");
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    ErrorMessage = ex.Message,
                };
                return View("Error", errorModel);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] AuthenticationModel user)
        {
            try
            {
                long result = this._userService.Login(user.Username, user.Password);
                if (result == -2)
                {
                    throw new Exception("User doesn't exist");
                }
                else if (result == -1)
                {
                    throw new Exception("Password is incorrect");
                }

                HttpContext.Session.SetString("UserId", result.ToString());
                // at this point, the register is successful
                // here you redirect to Main page (Dashboard)
                return RedirectToAction("Dashboard", "Dashboard");

            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    ErrorMessage = ex.Message,
                };
                return View("Error", errorModel);
            }
        }
    }
}
