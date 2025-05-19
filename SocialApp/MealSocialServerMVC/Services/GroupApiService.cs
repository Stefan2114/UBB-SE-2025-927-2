using MealSocialServerMVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MealSocialServerMVC.Services
{
    public class GroupApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5281/groups";

        public GroupApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Group>>(BaseUrl);
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Group>($"{BaseUrl}/{id}");
        }

        public async Task CreateGroupAsync(Group group)
        {
            await _httpClient.PostAsJsonAsync(BaseUrl, group);
        }

        public async Task UpdateGroupAsync(Group group)
        {
            await _httpClient.PutAsJsonAsync($"{BaseUrl}/{group.Id}", group);
        }

        public async Task DeleteGroupAsync(int id)
        {
            await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        }
    }
} 