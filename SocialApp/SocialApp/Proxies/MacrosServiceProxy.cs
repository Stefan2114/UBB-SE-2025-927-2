using AppCommonClasses.Models;
using AppCommonClasses.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;

namespace SocialApp.Proxies
{
    // This proxy implements IMacrosService and handles HTTP calls to the API.
    public class MacrosServiceProxy : IMacrosService
    {
        private readonly HttpClient _httpClient;

        public MacrosServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Helper to get all macros for a user, with status code check
        private List<Macros> GetMacrosListByUserId(long userId)
        {
            var response = _httpClient.GetAsync("macros/user/" + userId).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var macrosList = response.Content.ReadFromJsonAsync<List<Macros>>().GetAwaiter().GetResult();
                return macrosList ?? new List<Macros>();
            }
            else
            {
                throw new Exception($"Failed to fetch macros: {response.StatusCode}");
            }
        }

        public double GetProteinIntake(long userId)
        {
            var macrosList = GetMacrosListByUserId(userId);
            return macrosList.Sum(m => m.TotalProtein ?? 0);
        }

        public double GetCarbohydratesIntake(long userId)
        {
            var macrosList = GetMacrosListByUserId(userId);
            return macrosList.Sum(m => m.TotalCarbohydrates ?? 0);
        }

        public double GetFatIntake(long userId)
        {
            var macrosList = GetMacrosListByUserId(userId);
            return macrosList.Sum(m => m.TotalFat ?? 0);
        }

        public double GetFiberIntake(long userId)
        {
            var macrosList = GetMacrosListByUserId(userId);
            return macrosList.Sum(m => m.TotalFiber ?? 0);
        }

        public double GetSugarIntake(long userId)
        {
            var macrosList = GetMacrosListByUserId(userId);
            return macrosList.Sum(m => m.TotalSugar ?? 0);
        }
    }
}
