using AppCommonClasses.DTOs;
using AppCommonClasses.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using Server.DTOs;

namespace SocialApp.Proxies
{
    public class BodyMetricServiceProxy : IBodyMetricService
    {
        private readonly HttpClient httpClient;

        public BodyMetricServiceProxy()
        {
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7106/bodymetrics/")
            };
        }

        public void UpdateUserBodyMetrics(string username, float weight, float height, float? targetWeight)
        {
            try
            {
                Debug.WriteLine($"Updating body metrics for user: {username}");

                var bodyMetricDTO = new BodyMetricDTO
                {
                    Username = username,
                    Weight = weight,
                    Height = height,
                    TargetWeight = targetWeight
                };

                // Use GetAwaiter().GetResult() for better exception handling
                var response = httpClient.PutAsJsonAsync("update", bodyMetricDTO)
                    .GetAwaiter().GetResult();

                // Check for success
                response.EnsureSuccessStatusCode();
                Debug.WriteLine("Body metrics updated successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateUserBodyMetrics: {ex}");
                throw;
            }
        }
    }
}
