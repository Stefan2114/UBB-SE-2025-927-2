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

        public Calorie GetCaloriesByUserId(long userId)
        {
            return this.httpClient.GetFromJsonAsync<Calorie>($"calories/{userId}").Result!;
        }

        public void SetDailyIntake(long userId, double intake)
        {
            var response = this.httpClient.PutAsJsonAsync($"calories/{userId}/daily-intake", intake).Result;
            response.EnsureSuccessStatusCode();
        }

        public void SetCaloriesConsumed(long userId, double consumed)
        {
            var response = this.httpClient.PutAsJsonAsync($"calories/{userId}/calories-consumed", consumed).Result;
            response.EnsureSuccessStatusCode();
        }

        public void SetCaloriesBurned(long userId, double burned)
        {
            var response = this.httpClient.PutAsJsonAsync($"calories/{userId}/calories-burned", burned).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
