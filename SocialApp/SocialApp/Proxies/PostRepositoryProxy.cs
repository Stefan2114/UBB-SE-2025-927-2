

namespace SocialApp.Proxies
{
    using AppCommonClasses.Enums;
    using AppCommonClasses.Models;
    using AppCommonClasses.Repos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    public class PostRepositoryProxy : IPostRepository
    {

        private readonly HttpClient httpClient;

        public PostRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/posts/");
        }
        public void DeletePostById(long postId)
        {
            this.httpClient.DeleteAsync($"{postId}").Wait();
        }

        public List<Post> GetAllPosts()
        {
            return this.httpClient.GetFromJsonAsync<List<Post>>("").Result!;
        }

        public Post GetPostById(long postId)
        {
            return this.httpClient.GetFromJsonAsync<Post>($"{postId}").Result!;
        }

        public List<Post> GetPostsByGroupId(long groupId)
        {
            return this.httpClient.GetFromJsonAsync<List<Post>>($"group/{groupId}").Result!;
        }

        public List<Post> GetPostsByUserId(long userId)
        {
            return this.httpClient.GetFromJsonAsync<List<Post>>($"user/{userId}").Result!;
        }

        public List<Post> GetPostsGroupsFeed(long userId)
        {
            return this.httpClient.GetFromJsonAsync<List<Post>>($"groupfeed/{userId}").Result!;
        }

        public List<Post> GetPostsHomeFeed(long userId)
        {
            return this.httpClient.GetFromJsonAsync<List<Post>>($"homefeed/{userId}").Result!;
        }

        public void SavePost(Post post)
        {
            this.httpClient.PostAsJsonAsync("", post).Wait();
        }

        public void UpdatePostById(long postId, string title, string content, PostVisibility visibility, PostTag tag)
        {
            var post = new PostDTO
            {
                Title = title,
                Content = content,
                Visibility = visibility,
                Tag = tag
            };

            httpClient.PutAsJsonAsync($"{postId}", post).Wait();
        }
    }
}
