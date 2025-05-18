namespace SocialApp.Proxies
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class MacrosRepositoryProxy : IMacrosRepository
    {
        private readonly HttpClient httpClient;

        public MacrosRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/macros/");
        }

        public void DeleteMacrosById(long id)
        {
            this.httpClient.DeleteAsync($"{id}").Wait();
        }

        public List<Macros> GetAllMacros()
        {
            return this.httpClient.GetFromJsonAsync<List<Macros>>("").Result!;
        }

        public Macros GetMacrosById(long id)
        {
            return this.httpClient.GetFromJsonAsync<Macros>($"{id}").Result!;
        }

        public List<Macros> GetMacrosByUserId(long userId)
        {
            return this.httpClient.GetFromJsonAsync<List<Macros>>($"user/{userId}").Result!;
        }

        public void SaveMacros(Macros macros)
        {
            this.httpClient.PostAsJsonAsync("", macros).Wait();
        }

        public void UpdateMacrosById(long id, Macros updatedMacros)
        {
            this.httpClient.PutAsJsonAsync($"{id}", updatedMacros).Wait();
        }
    }
}
