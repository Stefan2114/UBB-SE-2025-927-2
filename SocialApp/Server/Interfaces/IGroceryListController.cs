using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface IGroceryListController
    {
        Task<ActionResult<List<GroceryIngredient>>> GetIngredientsForUser(int userId);

        Task<IActionResult> UpdateIsChecked(int userId, int ingredientId, [FromBody] bool isChecked);

        Task<ActionResult<GroceryIngredient>> AddIngredientToUser(int userId, [FromBody] GroceryIngredient request);
    }
}