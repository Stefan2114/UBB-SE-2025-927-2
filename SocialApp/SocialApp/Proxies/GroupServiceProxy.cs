using System.Net.Http.Json;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;


namespace SocialApp.Proxies
{
    public class GroupServiceProxy : IGroupService
    {
        private readonly HttpClient httpClient;
        private const string BaseUrl = "http://localhost:5281/groups";

        public GroupServiceProxy()
        {
            this.httpClient = new HttpClient();
        }

        public Group GetGroupById(long id)
        {
            var response = httpClient.GetAsync($"{BaseUrl}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Group>().Result;
            }
            throw new Exception($"Failed to get group: {response.StatusCode}");
        }

        public List<Group> GetGroups(long userId)
        {
            var response = httpClient.GetAsync($"{BaseUrl}/user/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<Group>>().Result;
            }
            throw new Exception($"Failed to get groups: {response.StatusCode}");
        }

        public List<User> GetUsersFromGroup(long groupId)
        {
            var response = httpClient.GetAsync($"{BaseUrl}/{groupId}/users").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<User>>().Result;
            }
            throw new Exception($"Failed to get users from group: {response.StatusCode}");
        }

        public Group AddGroup(string name, string desc, string image, long adminId)
        {
            var group = new Group
            {
                Name = name,
                Description = desc,
                Image = image,
                AdminId = adminId
            };

            var response = httpClient.PostAsJsonAsync(BaseUrl, group).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Group>().Result;
            }
            throw new Exception($"Failed to add group: {response.StatusCode}");
        }

        public void DeleteGroup(long groupId)
        {
            var response = httpClient.DeleteAsync($"{BaseUrl}/{groupId}").Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete group: {response.StatusCode}");
            }
        }

        public void UpdateGroup(long id, string name, string desc, string image, long adminId)
        {
            var group = new Group
            {
                Name = name,
                Description = desc,
                Image = image,
                AdminId = adminId
            };

            var response = httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", group).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update group: {response.StatusCode}");
            }
        }

        public List<Group> GetAllGroups()
        {
            var response = httpClient.GetAsync(BaseUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<Group>>().Result;
            }
            throw new Exception($"Failed to get all groups: {response.StatusCode}");
        }
    }
} 