using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<GroceryIngredient>> AddIngredientToUser(int userId, [FromBody] GroceryIngredient request)
        {
            var result = await this.groceryRepository.AddIngredientToUser(userId, request);
            return this.Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<GroceryIngredient>>> GetIngredientsForUser(int userId)
        {
            var result = await this.groceryRepository.GetIngredientsForUser(userId);
            return this.Ok(result);
        }

        [HttpPost("{userId}/ingredient/{ingredientId}/check")]
        public async Task<IActionResult> UpdateIsChecked(int userId, int ingredientId, [FromBody] bool isChecked)
        {
            var ingredients = await this.groceryRepository.GetIngredientsForUser(userId);
            foreach (var ingredient in ingredients)
            {
                ingredient.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                    {
                        var item = s as GroceryIngredient ?? GroceryIngredient.defaultIngredient;
                        _ = this.UpdateIsChecked(userId, item.Id, item.IsChecked);
                    }
                };
            }

            return this.NoContent();
        }
    }
}
