using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("meals")]
    public class MealsController : ControllerBase, IMealsController
    {
        private readonly IMealRepository mealRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MealsController"/> class.
        /// </summary>
        /// <param name="mealRepository"></param>
        public MealsController(IMealRepository mealRepository)
        {
            this.mealRepository = mealRepository;
        }

        [HttpPost]
        public IActionResult CreateMeal([FromBody] Meal meal)
        {
            this.mealRepository.CreateMealAsync(meal);
            return this.Ok();
        }

        [HttpGet]
        public ActionResult<List<Meal>> GetAllMeals()
        {
            return this.mealRepository.GetAllMeals();
        }
    }
}