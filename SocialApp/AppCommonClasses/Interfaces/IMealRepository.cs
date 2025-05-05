namespace AppCommonClasses.Interfaces
{
    using AppCommonClasses.Models;
    using System.Threading.Tasks;

    public interface IMealRepository
    {
        void CreateMealAsync(Meal meal);
        void AddMealIngredientAsync(int mealId, int ingredientId, float quantity);
        List<Meal> GetAllMeals();
    }
}