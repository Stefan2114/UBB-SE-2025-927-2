namespace SocialApp.Proxies
{
    using AppCommonClasses.Models;
    using AppCommonClasses.Interfaces;
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;

    public class CalorieRepositoryProxy : ICalorieRepository
    {
        private readonly HttpClient httpClient;

        public CalorieRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/calories/");
        }

        // Synchronous call using .Result or .GetAwaiter().GetResult()
        public Calorie GetCaloriesByUserId(int userId)
        {
            // Get the calorie data from the API synchronously
            return this.httpClient.GetFromJsonAsync<Calorie>($"user/{userId}").Result!;
        }
    }
}
