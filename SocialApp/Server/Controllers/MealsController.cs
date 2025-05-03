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
        /*
         * Due to some validation for the fields, I live here an example for testing POST
        {
          "name": "Chicken Salad",
          "ingredients": "Chicken, Lettuce, Tomatoes",
          "calories": 350,
          "category": "Salad",
          "protein": 30,
          "carbohydrates": 10,
          "fat": 15,
          "fiber": 5,
          "sugar": 3,
          "photoLink": "http://example.com/photo.jpg",
          "recipe": "Mix all ingredients and serve cold.",
          "preparationTime": 15.5,
          "servings": 2,
          "createdAt": "2024-05-02T10:30:00Z",
          "cookingLevel": "Beginner",
          "image": "/9j/4A==",
          "imagePath": "images/chickensalad.jpg"
        }
        */


        [HttpGet]
        public ActionResult<List<Meal>> GetAllMeals()
        {
            return this.mealRepository.GetAllMeals();
        }
    }
}