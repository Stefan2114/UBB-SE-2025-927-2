namespace Server.Controllers
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("meals")]
    public class MealController : ControllerBase
    {
        private readonly IMealService mealService;

        public MealController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Meal>>> GetAllMeals()
        {
            var meals = await mealService.RetrieveAllMealsAsync();
            return Ok(meals);
        }

        [HttpGet("ingredient/{ingredientName}")]
        public async Task<ActionResult<Ingredient>> GetIngredientByName(string ingredientName)
        {
            var ingredient = await mealService.RetrieveIngredientByNameAsync(ingredientName);
            if (ingredient == null)
                return NotFound();
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateMeal([FromBody] Meal meal)
        {
            var result = await mealService.CreateMealWithCookingLevelAsync(meal,"Default");
            if (result)
                return Ok(1);
            return BadRequest();
        }

        [HttpPost("create-with-level")]
        public async Task<ActionResult> CreateMealWithCookingLevel([FromBody] Meal meal, [FromQuery] string cookingLevelDescription)
        {
            var result = await mealService.CreateMealWithCookingLevelAsync(meal, cookingLevelDescription);
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPost("addingredient")]
        public async Task<ActionResult> AddIngredientToMeal([FromBody] AddIngredientRequest request)
        {
            var result = await mealService.AddIngredientToMealAsync(request.MealIdentifier, request.IngredientIdentifier, request.IngredientQuantity);
            if (result)
                return Ok();
            return BadRequest();
        }
    }

    public class AddIngredientRequest
    {
        public int MealIdentifier { get; set; }
        public int IngredientIdentifier { get; set; }
        public float IngredientQuantity { get; set; }
    }
}
