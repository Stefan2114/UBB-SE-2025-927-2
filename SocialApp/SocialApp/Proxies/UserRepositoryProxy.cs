namespace SocialApp.Proxies
{
    using AppCommonClasses.DTOs;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;

    public class UserRepositoryProxy : IUserRepository
    {
        private readonly HttpClient httpClient;

        public UserRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/users/");
        }

        public void DeleteById(long id)
        {
            this.httpClient.DeleteAsync($"{id}").Wait();
        }

        public void Follow(long userId, long whoToFollowId)
        {
            this.httpClient.PostAsJsonAsync($"users/{userId}/follow/{whoToFollowId}", "").Wait();
        }

        public List<User> GetAll()
        {
            return this.httpClient.GetFromJsonAsync<List<User>>("").Result!;
        }

        public User GetByEmail(string email)
        {
            return this.httpClient.GetFromJsonAsync<User>($"users/{email}").Result!;
        }

        public User GetById(long id)
        {
            return this.httpClient.GetFromJsonAsync<User>($"{id}").Result!;
        }

        public User GetByUsername(string username)
        {
            Debug.WriteLine($"Getting in proxy user by username: {username}");
            return this.httpClient.GetFromJsonAsync<User>($"users/{username}").Result!;
            Debug.WriteLine($"Got in proxy user by username: {username}");
        }

        public List<User> GetUserFollowers(long id)
        {
            return this.httpClient.GetFromJsonAsync<List<User>>($"users/{id}/followers").Result!;
        }

        public List<User> GetUserFollowing(long id)
        {
            return this.httpClient.GetFromJsonAsync<List<User>>($"users/{id}/following").Result!;
        }

        public void Save(User entity)
        {
            this.httpClient.PostAsJsonAsync("", entity).Wait();
        }

        public void Unfollow(long userId, long whoToUnfollowId)
        {
            this.httpClient.DeleteAsync($"users/{userId}/unfollow/{whoToUnfollowId}").Wait();
        }

        public void UpdateById(long id, string username, string email, string hashPassword, string image)
        {
            var user = new UserModelDTO
            {
                Name = username,
                Email = email,
                HashPassword = hashPassword,
                Image = image
            };
            this.httpClient.PutAsJsonAsync($"{id}", user).Wait();
        }
    }
}
