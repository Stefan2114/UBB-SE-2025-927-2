using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface IGroceryListRepository
    {
        Task<List<GroceryIngredient>> GetIngredientsForUser(long userId);
        Task UpdateIsChecked(long userId, int ingredientId, bool isChecked);
        Task<GroceryIngredient> AddIngredientToUser(long userId, GroceryIngredient ingredient);
    }
}
