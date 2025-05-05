namespace SocialApp.Interfaces
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using AppCommonClasses.Models;

    public interface IGroceryListService
    {
        Task<List<GroceryIngredient>> GetIngredientsForUser(int userId);

        Task UpdateIsChecked(int userId, int ingredientId, bool isChecked);

        Task<GroceryIngredient> AddIngredientToUser(int userId, GroceryIngredient ingredient, string newGroceryIngredientName, ObservableCollection<SectionModel> section);
    }
}