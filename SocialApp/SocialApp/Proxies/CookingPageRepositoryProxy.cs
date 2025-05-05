using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

public class CookingSkillDTO
{
    public int CookingSkillId { get; set; }
}

namespace SocialApp.Proxies
{
    public class CookingPageRepositoryProxy : ICookingPageRepository
    {
        private readonly HttpClient httpClient;

        public CookingPageRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://localhost:7106/cooking/");
        }

        public List<CookingSkill> GetAllCookingSkills()
        {
            try
            {
                var response = httpClient.GetAsync("skills").GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                return response.Content.ReadFromJsonAsync<List<CookingSkill>>().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAllCookingSkills: {ex}");
                throw;
            }
        }

        public int GetUserIdByName(string firstName, string lastName)
        {
            try
            {
                var response = httpClient.GetAsync($"user/{firstName}/{lastName}")
                    .GetAwaiter().GetResult();
                
                response.EnsureSuccessStatusCode();
                return response.Content.ReadFromJsonAsync<int>().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetUserIdByName: {ex}");
                throw;
            }
        }

        public int GetCookingSkillIdByDescription(string description)
        {
            try
            {
                var response = httpClient.GetAsync($"skill/{Uri.EscapeDataString(description)}")
                    .GetAwaiter().GetResult();
                
                response.EnsureSuccessStatusCode();
                return response.Content.ReadFromJsonAsync<int>().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetCookingSkillIdByDescription: {ex}");
                throw;
            }
        }

        public void UpdateUserCookingSkill(int userId, int cookingSkillId)
        {
            try
            {
                var cookingSkillDto = new CookingSkillDTO
                {
                    CookingSkillId = cookingSkillId
                };

                var response = httpClient.PutAsJsonAsync($"user/{userId}", cookingSkillDto)
                    .GetAwaiter().GetResult();

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in UpdateUserCookingSkill: {ex}");
                throw;
            }
        }
    }
}
