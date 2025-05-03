using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface IGroceryListRepository
    {
        Task<List<GroceryIngredient>> GetIngredientsForUser(int userId);
        Task UpdateIsChecked(int userId, int ingredientId, bool isChecked);
        Task<GroceryIngredient> AddIngredientToUser(int userId, GroceryIngredient ingredient);
    }
}
