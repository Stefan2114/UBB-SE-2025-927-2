namespace SocialApp.Interfaces
{
    using AppCommonClasses.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMealService
    {
        Task<bool> CreateMealWithCookingLevelAsync(Meal mealToCreate, string cookingLevelDescription);

        Task<List<Meal>> RetrieveAllMealsAsync();

        Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName);

        Task<int> CreateMealAsync(Meal mealToCreate);

        Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity);
    }
}