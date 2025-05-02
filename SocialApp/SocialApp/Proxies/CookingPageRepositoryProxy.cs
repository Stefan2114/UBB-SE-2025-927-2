using AppCommonClasses.Enums;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Proxies
{
    public class CookingPageRepositoryProxy : ICookingPageRepository
    {
        private readonly HttpClient httpClient;

        public CookingPageRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/cooking/");
        }

        public CookingPage GetByUserId(int userId)
        {
            return this.httpClient.GetFromJsonAsync<CookingPage>($"user/{userId}").Result;
        }

        public void UpdateUserCookingSkill(int userId, int cookingSkillId)
        {
            this.httpClient.PutAsync($"user/{userId}/skill/{cookingSkillId}", null).Wait();
        }

        public CookingSkill GetCookingSkillByDescription(string description)
        {
            return this.httpClient.GetFromJsonAsync<CookingSkill>($"skill/{description}").Result;
        }
    }
}
