using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface IIngredientRepository
    {
        // Async method to fetch ingredient details by name
        Task<Ingredient?> GetIngredientByNameAsync(string name);
    }
}
