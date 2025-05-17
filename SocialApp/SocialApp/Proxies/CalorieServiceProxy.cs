using System;
using System.Net.Http;
using System.Net.Http.Json;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

namespace MealSocialServerMVC.Proxies
{
    public class CalorieServiceProxy : ICalorieService
    {
        private readonly HttpClient httpClient;

        public CalorieServiceProxy()
        {
            // Create HttpClientHandler to bypass SSL validation (only for development)
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            this.httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7106/calories/")
            };
        }

        public double GetGoal(long userId)
        {
            try
            {
                var response = httpClient.GetAsync($"goal/{userId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<double>().Result;
                }

                return 2000; // Default goal if request fails
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting calorie goal: {ex.Message}");
                return 2000; // Default goal on exception
            }
        }

        public double GetFood(long userId)
        {
            try
            {
                var response = httpClient.GetAsync($"food/{userId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<double>().Result;
                }

                return 0; // Default consumed if request fails
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting calories consumed: {ex.Message}");
                return 0; // Default consumed on exception
            }
        }

        public double GetExercise(long userId)
        {
            try
            {
                var response = httpClient.GetAsync($"exercise/{userId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<double>().Result;
                }

                return 0; // Default burned if request fails
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting calories burned: {ex.Message}");
                return 0; // Default burned on exception
            }
        }
    }
}
