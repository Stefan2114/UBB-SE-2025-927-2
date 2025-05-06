using AppCommonClasses.DTOs;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SocialApp.Proxies
{
    /// <summary>
    /// A proxy that communicates with the user microservice and implements IUserRepository.
    /// Provides user data handling and relationship management.
    /// </summary>
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
            var response = this.httpClient.DeleteAsync($"{id}").Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to delete user {id}. Status: {response.StatusCode}");
            }
        }

        public void Follow(long userId, long whoToFollowId)
        {
            var response = this.httpClient.PostAsJsonAsync($"users/{userId}/follow/{whoToFollowId}", "").Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to follow. Status: {response.StatusCode}");
            }
        }

        public List<User> GetAll()
        {
            var response = this.httpClient.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<User>>().Result ?? new List<User>();
            }

            Debug.WriteLine($"Failed to get users. Status: {response.StatusCode}");
            return new List<User>();
        }

        public User GetByEmail(string email)
        {
            var response = this.httpClient.GetAsync($"users/{email}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<User>().Result;
            }

            Debug.WriteLine($"User not found by email {email}. Status: {response.StatusCode}");
            return null;
        }

        public User GetById(long id)
        {
            var response = this.httpClient.GetAsync($"{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<User>().Result;
            }

            Debug.WriteLine($"User not found by ID {id}. Status: {response.StatusCode}");
            return null;
        }

        public User GetByUsername(string username)
        {
            Debug.WriteLine($"Getting in proxy user by username: {username}");
            var response = this.httpClient.GetAsync($"users/{username}").Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadFromJsonAsync<User>().Result;
                Debug.WriteLine($"Got in proxy user by username: {username}");
                return user;
            }

            Debug.WriteLine($"User not found by username {username}. Status: {response.StatusCode}");
            return null;
        }

        public List<User> GetUserFollowers(long id)
        {

            var response = this.httpClient.GetAsync($"users/{id}/followers").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<User>>().Result ?? new List<User>();
            }

            Debug.WriteLine($"Failed to get followers for user {id}. Status: {response.StatusCode}");
            return new List<User>();
        }

        public List<User> GetUserFollowing(long id)
        {
            var response = this.httpClient.GetAsync($"users/{id}/following").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<User>>().Result ?? new List<User>();
            }

            Debug.WriteLine($"Failed to get following for user {id}. Status: {response.StatusCode}");
            return new List<User>();
        }

        public void Save(User entity)
        {
            var response = this.httpClient.PostAsJsonAsync("", entity).Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to save user. Status: {response.StatusCode}");
            }
        }

        public void Unfollow(long userId, long whoToUnfollowId)
        {
            var response = this.httpClient.DeleteAsync($"users/{userId}/unfollow/{whoToUnfollowId}").Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to unfollow. Status: {response.StatusCode}");
            }
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

            var response = this.httpClient.PutAsJsonAsync($"{id}", user).Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to update user {id}. Status: {response.StatusCode}");
            }
        }
    }
}
