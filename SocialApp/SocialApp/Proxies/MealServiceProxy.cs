using AppCommonClasses.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppCommonClasses.Interfaces;

namespace SocialApp.Proxies
{
    public class MealServiceProxy : IMealService
    {
        private readonly HttpClient _httpClient;

        public MealServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateMealWithCookingLevelAsync(Meal mealToCreate, string cookingLevelDescription)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"api/meals/create-with-level?cookingLevelDescription={cookingLevelDescription}", mealToCreate);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Meal>> RetrieveAllMealsAsync()
        {
            var response = await _httpClient.GetAsync("api/meals");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Meal>>() ?? new List<Meal>();
            return new List<Meal>();
        }

        public async Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName)
        {
            var response = await _httpClient.GetAsync($"api/meals/ingredient/{ingredientName}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Ingredient>();
            return null;
        }

        public async Task<int> CreateMealAsync(Meal mealToCreate)
        {
            var response = await _httpClient.PostAsJsonAsync("api/meals", mealToCreate);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<int>();
            return -1;
        }

        public async Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/meals/addingredient",
                new { mealIdentifier, ingredientIdentifier, ingredientQuantity });
            return response.IsSuccessStatusCode;
        }
    }
}
