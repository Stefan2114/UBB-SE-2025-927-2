namespace Server.Interfaces
{
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    using Server.DTOs;

    public interface IGroceryListController
    {
        Task<ActionResult<List<GroceryIngredient>>> GetIngredientsForUser(long userId);

        Task<IActionResult> UpdateIsChecked(long userId, int ingredientId, [FromBody] bool isChecked);

        Task<ActionResult<GroceryIngredient>> AddIngredientToUser(long userId, [FromBody] GroceryIngredientDTO request);
    }
}