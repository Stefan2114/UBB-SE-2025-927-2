namespace SocialApp.Interfaces
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using AppCommonClasses.Models;

    public interface IGroceryListService
    {
        Task<List<GroceryIngredient>> GetIngredientsForUser(long userId);

        Task UpdateIsChecked(long userId, int ingredientId, bool isChecked);

        Task<GroceryIngredient> AddIngredientToUser(long userId, GroceryIngredient ingredient, string newGroceryIngredientName, ObservableCollection<SectionModel> section);
    }
}