using AppCommonClasses.Models;
using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    public class MacrosController : Controller
    {
        private readonly IMacrosRepository _macrosRepository;

        public MacrosController(IMacrosRepository macrosRepository)
        {
            _macrosRepository = macrosRepository;
        }

        public IActionResult Macros(long userId)
        {
            var macros = _macrosRepository.GetMacrosByUserId(userId);
            ViewBag.UserId = userId;
            return View(macros);
        }

    }
}
