namespace SocialApp.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using AppCommonClasses.Enums;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;

    public class MealRepositoryProxy : IMealRepository
    {
        private readonly HttpClient httpClient;

        public MealRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/meals/");
        }

        public void AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var mealIngredient = new MealIngredient
            {
                MealId = mealId,
                IngredientId = ingredientId,
                Quantity = quantity,
            };
            this.httpClient.PostAsJsonAsync("ingredients", mealIngredient).Wait();
        }

        public void CreateMealAsync(Meal meal)
        {
            this.httpClient.PostAsJsonAsync("", meal).Wait();
        }

        public List<Meal> GetAllMeals()
        {
            return this.httpClient.GetFromJsonAsync<List<Meal>>("").Result;
        }
    }
}
