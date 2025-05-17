namespace SocialApp.Proxies
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using AppCommonClasses.DTOs;
    using AppCommonClasses.Interfaces;
    using Server.DTOs;

    public class BodyMetricRepositoryProxy : IBodyMetricRepository
    {
        private readonly HttpClient httpClient;

        public BodyMetricRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/bodymetrics/");
        }

        public void UpdateUserBodyMetrics(long userId, float weight, float height, float? targetWeight)
        {
            try
            {
                var bodyMetricDTO = new BodyMetricDTO
                {
                    Weight = weight,
                    Height = height,
                    TargetWeight = targetWeight,
                };

                //// Instead of Wait(), use GetAwaiter().GetResult() which handles exceptions better
                //var response = httpClient.PutAsJsonAsync($"user/{userId}", bodyMetricDTO)
                //    .GetAwaiter().GetResult();

                //// Check for success
                //response.EnsureSuccessStatusCode();

                httpClient.PutAsJsonAsync($"user/{userId}", bodyMetricDTO).Wait();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateUserBodyMetrics: {ex}");
                throw;
            }
        }
    }
}
