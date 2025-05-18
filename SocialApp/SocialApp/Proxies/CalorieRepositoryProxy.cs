//namespace SocialApp.Proxies
//{
//    using System;
//    using System.Diagnostics;
//    using System.Net.Http;
//    using System.Net.Http.Json;
//    using AppCommonClasses.Interfaces;
//    using AppCommonClasses.Models;
//    using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

//    public class CalorieRepositoryProxy : ICalorieRepository
//    {
//        private readonly HttpClient httpClient;

//        public CalorieRepositoryProxy()
//        {
//            this.httpClient = new HttpClient();
//            this.httpClient.BaseAddress = new Uri("https://localhost:7106/");
//        }

//        // Synchronous call using .Result or .GetAwaiter().GetResult()
//        public Calorie GetCaloriesByUserId(long userId)
//        {
//            // Get the calorie data from the API synchronously
//            var response = this.httpClient.GetAsync($"calories/{userId}").Result!;
//            if (!response.IsSuccessStatusCode)
//            {
//                return null;
//            }
//            return response.Content.ReadFromJsonAsync<Calorie>().Result;

//        }
//    }
//}
