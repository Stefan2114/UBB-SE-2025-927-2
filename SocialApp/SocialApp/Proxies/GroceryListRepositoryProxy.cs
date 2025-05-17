using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

namespace SocialApp.Proxies
{
    public class GroceryListRepositoryProxy : IGroceryListRepository
    {
        private readonly HttpClient httpClient;

        public GroceryListRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/grocery-list/");
        }

        public async Task<GroceryIngredient> AddIngredientToUser(long userId, GroceryIngredient ingredient)
        {
            var request = new
            {
                Name = ingredient.Name,
                IsChecked = ingredient.IsChecked,
            };

            var response = await this.httpClient.PostAsJsonAsync($"{userId}/add", request);
            if (!response.IsSuccessStatusCode)
            {
                return GroceryIngredient.defaultIngredient;
            }

            var result = await response.Content.ReadFromJsonAsync<GroceryIngredient>();
            return result ?? GroceryIngredient.defaultIngredient;
        }

        public async Task<List<GroceryIngredient>> GetIngredientsForUser(long userId)
        {
            var response = await this.httpClient.GetAsync($"{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<GroceryIngredient>();
            }

            var ingredients = await response.Content.ReadFromJsonAsync<List<GroceryIngredient>>();
            foreach (var ingredient in ingredients)
            {
                ingredient.Id = userId;
            }

            return ingredients ?? new List<GroceryIngredient>();
        }

        public async Task UpdateIsChecked(long userId, int ingredientId, bool isChecked)
        {
            var response = await this.httpClient.PostAsJsonAsync(
                $"{userId}/ingredient/{ingredientId}/check",
                isChecked);

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Server error");
            }
        }
    }
}
