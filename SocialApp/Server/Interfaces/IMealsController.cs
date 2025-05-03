using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface IMealsController
    {
        public IActionResult CreateMeal([FromBody] Meal meal);
        public ActionResult<List<Meal>> GetAllMeals();

    }
}