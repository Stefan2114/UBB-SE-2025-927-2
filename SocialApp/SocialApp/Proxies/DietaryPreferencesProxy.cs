using System;
using System.Net.Http;
using System.Net.Http.Json;
using AppCommonClasses.Interfaces;

namespace SocialApp.Proxies
{
    public class DietaryPreferencesProxy : IDietaryPreferencesRepository
    {
        private readonly HttpClient httpClient;

        public DietaryPreferencesProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/dietarypreferences/");
        }

        public void AddUserDietaryPreferenceIfNotExists(long userId, string dietaryPreference)
        {
            try
            {
                var response = httpClient.PostAsync($"{userId}?dietaryPreference={dietaryPreference}", null).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in AddUserDietaryPreferenceIfNotExists: {ex}");
                throw;
            }
        }

        public string GetUserDietaryPreference(long userId)
        {
            try
            {
                var response = httpClient.GetAsync($"{userId}").GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadFromJsonAsync<string>().GetAwaiter().GetResult();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetUserDietaryPreference: {ex}");
                throw;
            }
        }

        public void UpdateUserDietaryPreference(long userId, string newDietaryPreference)
        {
            try
            {
                var response = httpClient.PutAsync($"update/{userId}?newDietaryPreference={newDietaryPreference}", null).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateUserDietaryPreference: {ex}");
                throw;
            }
        }

        public void RemoveUserDietaryPreference(long userId)
        {
            try
            {
                var response = httpClient.DeleteAsync($"remove/{userId}").GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RemoveUserDietaryPreference: {ex}");
                throw;
            }
        }
    }
} 