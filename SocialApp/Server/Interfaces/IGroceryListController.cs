using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface IGroceryListController
    {
        Task<ActionResult<List<GroceryIngredient>>> GetIngredientsForUser(long userId);

        Task<IActionResult> UpdateIsChecked(long userId, int ingredientId, [FromBody] bool isChecked);

        Task<ActionResult<GroceryIngredient>> AddIngredientToUser(long userId, [FromBody] GroceryIngredient request);
    }
}