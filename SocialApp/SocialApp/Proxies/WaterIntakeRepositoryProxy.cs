using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Proxies
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using AppCommonClasses.Interfaces;

    public class WaterIntakeRepositoryProxy : IWaterIntakeRepository
    {
        private readonly HttpClient httpClient;

        public WaterIntakeRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/waterintake/");
        }

        public void AddUserIfNotExists(long userId)
        {
            try
            {
                var response = httpClient.PostAsync($"{userId}", null).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in AddUserIfNotExists: {ex}");
                throw;
            }
        }

        public double GetWaterIntake(long userId)
        {
            try
            {
                var response = httpClient.GetAsync($"{userId}").GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadFromJsonAsync<float>().GetAwaiter().GetResult();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetWaterIntake: {ex}");
                throw;
            }
        }

        public void UpdateWaterIntake(long userId, double newIntake)
        {
            try
            {
                var response = httpClient.PutAsync($"update/{userId}?newIntake={newIntake}", null).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateWaterIntake: {ex}");
                throw;
            }
        }

        public void RemoveWater300(long userId) => RemoveWater(userId, "300");
        public void RemoveWater400(long userId) => RemoveWater(userId, "400");
        public void RemoveWater500(long userId) => RemoveWater(userId, "500");
        public void RemoveWater750(long userId) => RemoveWater(userId, "750");

        private void RemoveWater(long userId, string amount)
        {
            try
            {
                var response = httpClient.DeleteAsync($"remove/{amount}/{userId}").GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RemoveWater{amount}: {ex}");
                throw;
            }
        }
    }
}
