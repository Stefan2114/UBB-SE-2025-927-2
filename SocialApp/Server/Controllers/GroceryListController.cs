using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("grocery-list")]
    public class GroceryListController : ControllerBase, IGroceryListController
    {
        private readonly IGroceryListRepository groceryRepository;

        public GroceryListController(IGroceryListRepository groceryRepository)
        {
            this.groceryRepository = groceryRepository;
        }

        [HttpPost("{userId}/add")]
        public async Task<ActionResult<GroceryIngredient>> AddIngredientToUser(long userId, [FromBody] GroceryIngredientDTO request)
        {
            GroceryIngredient groceryIngredient = new GroceryIngredient { Id = userId, IsChecked = request.IsChecked, Name = request.Name };
            var result = await this.groceryRepository.AddIngredientToUser(userId, groceryIngredient);
            return this.Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<GroceryIngredient>>> GetIngredientsForUser(long userId)
        {
            var result = await this.groceryRepository.GetIngredientsForUser(userId);
            return this.Ok(result);
        }

        [HttpPost("{userId}/ingredient/{ingredientId}/check")]
        public async Task<IActionResult> UpdateIsChecked(long userId, int ingredientId, [FromBody] bool isChecked)
        {
            var ingredients = await this.groceryRepository.GetIngredientsForUser(userId);
            var ingredient = ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);

            if (ingredient == null)
            {
                return this.NotFound();
            }

            ingredient.IsChecked = isChecked;
            await this.groceryRepository.UpdateIsChecked(userId, ingredientId, isChecked);

            return this.NoContent();
        }
    }
}
