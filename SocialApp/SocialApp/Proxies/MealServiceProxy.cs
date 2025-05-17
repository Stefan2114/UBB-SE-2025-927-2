using AppCommonClasses.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppCommonClasses.Interfaces;
using System.Net;

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

            if (response.IsSuccessStatusCode)
                return true;

            // Throw for web, return false for desktop
            throw new HttpRequestException($"Failed to create meal with cooking level. Status: {response.StatusCode}");
        }

        public async Task<List<Meal>> RetrieveAllMealsAsync()
        {
            var response = await _httpClient.GetAsync("api/meals");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Meal>>() ?? new List<Meal>();

            throw new HttpRequestException($"Failed to retrieve meals. Status: {response.StatusCode}");
        }

        public async Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName)
        {
            var response = await _httpClient.GetAsync($"api/meals/ingredient/{ingredientName}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Ingredient>();

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            throw new HttpRequestException($"Failed to retrieve ingredient. Status: {response.StatusCode}");
        }

        public async Task<int> CreateMealAsync(Meal mealToCreate)
        {
            var response = await _httpClient.PostAsJsonAsync("api/meals", mealToCreate);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<int>();

            throw new HttpRequestException($"Failed to create meal. Status: {response.StatusCode}");
        }

        public async Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/meals/addingredient",
                new { mealIdentifier, ingredientIdentifier, ingredientQuantity });

            if (response.IsSuccessStatusCode)
                return true;

            throw new HttpRequestException($"Failed to add ingredient to meal. Status: {response.StatusCode}");
        }
    }
}
