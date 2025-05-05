namespace SocialApp.Proxies
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;

    public class CalorieRepositoryProxy : ICalorieRepository
    {
        private readonly HttpClient httpClient;

        public CalorieRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/");
        }

        // Synchronous call using .Result or .GetAwaiter().GetResult()
        public Calorie GetCaloriesByUserId(long userId)
        {
            // Get the calorie data from the API synchronously
            return this.httpClient.GetFromJsonAsync<Calorie>($"calories/{userId}").Result!;

        }
    }
}
