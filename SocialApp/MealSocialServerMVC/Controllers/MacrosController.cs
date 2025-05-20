using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealSocialServerMVC.Controllers
{
    [ApiController]
    [Route("macros")]
    public class MacrosController : Controller
    {
        private readonly IMacrosRepository _macrosRepository;

        public MacrosController(IMacrosRepository macrosRepository)
        {
            _macrosRepository = macrosRepository;
        }
        [HttpGet]
        public IActionResult Macros()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            long userId = long.Parse(userIdString);
            var macros = _macrosRepository.GetMacrosByUserId(userId);
            ViewBag.UserId = userId;
            return View(macros);
        }

    }
}
