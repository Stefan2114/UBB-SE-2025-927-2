namespace SocialApp.Interfaces
{
    using AppCommonClasses.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public interface IGroceryListService
    {
        List<GroceryIngredient> GetIngredientsForUser(int userId);

        void UpdateIsChecked(int userId, int ingredientId, bool isChecked);

        GroceryIngredient AddIngredientToUser(int userId, GroceryIngredient ingredient, string newGroceryIngredientName, ObservableCollection<SectionModel> section);
    }
}