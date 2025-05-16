namespace SocialApp.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;

    public class WaterIntakeServiceProxy : IWaterIntakeService
    {
        private readonly HttpClient httpClient;

        public WaterIntakeServiceProxy()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7106/waterintake/"),
            };
        }

        public void AddUserIfNotExists(long userId)
        {
            // Try to get the water tracker for the user
            var response = this.httpClient.GetAsync($"{userId}").Result;

            if (response.IsSuccessStatusCode)
            {
                // Water tracker exists, nothing to do
                return;
            }

            // If not found, create a new water tracker
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var newWater = new Water
                {
                    U_Id = userId,
                    water_intake = 0
                };

                var postResponse = this.httpClient.PostAsJsonAsync("{userId}", newWater).Result;
                if (!postResponse.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Failed to create water tracker for user {userId}. Status: {postResponse.StatusCode}");
                }
            }
            else
            {
                Debug.WriteLine($"Unexpected response when checking water tracker for user {userId}: {response.StatusCode}");
            }
        }


        public float GetWaterIntake(long userId)
        {
            var response = this.httpClient.GetAsync($"water/{userId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var water = response.Content.ReadFromJsonAsync<Water>().Result;

                if (water != null) // Ensure water is not null before accessing its properties
                {
                    double water_intake = water.water_intake;
                    return (float)water_intake;
                }
                else
                {
                    Debug.WriteLine($"Water object is null for user ID {userId}.");
                }
            }
            else
            {
                Debug.WriteLine($"User not found by ID {userId}. Status: {response.StatusCode}");
            }

            return 0f;
        }

        public void RemoveWater300(long userId)
        {
            var removeResponse = this.httpClient.DeleteAsync("remove/300/{userId}").Result;

            if (!removeResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to remove water for user {userId}: {removeResponse.StatusCode}");
            }
        }

        public void RemoveWater400(long userId)
        {
            var removeResponse = this.httpClient.DeleteAsync("remove/400/{userId}").Result;

            if (!removeResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to remove water for user {userId}: {removeResponse.StatusCode}");
            }
        }

        public void RemoveWater500(long userId)
        {
            var removeResponse = this.httpClient.DeleteAsync("remove/500/{userId}").Result;

            if (!removeResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to remove water for user {userId}: {removeResponse.StatusCode}");
            }
        }

        public void RemoveWater750(long userId)
        {
            var removeResponse = this.httpClient.DeleteAsync("remove/750/{userId}").Result;

            if (!removeResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to remove water for user {userId}: {removeResponse.StatusCode}");
            }
        }

        public void UpdateWaterIntake(long userId, float newIntake)
        {

            var response = this.httpClient.PutAsJsonAsync($"water/{userId}", newIntake).Result;

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to update water intake for user {userId}. Status: {response.StatusCode}");
            }
        }
    }
}
