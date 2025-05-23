using AppCommonClasses.Models;
using AppCommonClasses.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    public class MacrosController : Controller
    {
        private readonly IMacrosService _macrosService;

        public MacrosController(IMacrosService macrosService)
        {
            // You may want to use dependency injection for HttpClient in production
            _macrosService = macrosService;
        }

        public IActionResult Macros(long userId)
        {
            var macros = _macrosService.GetMacrosListByUserId(userId);
            ViewBag.UserId = userId;
            return View(macros);
        }
    }
}
