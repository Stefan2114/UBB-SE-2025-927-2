namespace SocialApp.Proxies
{
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

    public class ReactionRepositoryProxy : IReactionRepository
    {
        private readonly HttpClient httpClient;

        public ReactionRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/reactions/");
        }

        public void DeleteByUserAndPost(long userId, long postId)
        {
            this.httpClient.DeleteAsync($"reactions/{userId}/{postId}").Wait();
        }

        public List<Reaction> GetAllReactions()
        {
            return this.httpClient.GetFromJsonAsync<List<Reaction>>("").Result!;
        }

        public List<Reaction> GetReactionsByPost(long postId)
        {
            return this.httpClient.GetFromJsonAsync<List<Reaction>>($"reactions/{postId}").Result!;
        }

        public Reaction GetReactionByUserAndPost(long userId, long postId)
        {
            return this.httpClient.GetFromJsonAsync<Reaction>($"reactions/{userId}/{postId}").Result!;
        }

        public void Save(Reaction reaction)
        {
            this.httpClient.PostAsJsonAsync("",reaction).Wait();
        }

        public void UpdateByUserAndPost(long userId, long postId, ReactionType type)
        {
            var reaction = new ReactionDTO { Type = type };

            this.httpClient.PostAsJsonAsync($"reactions/{userId}/{postId}",reaction).Wait();
        }
    }
}
