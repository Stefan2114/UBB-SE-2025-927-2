using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

namespace SocialApp.Proxies
{
    internal class CommentServiceProxy : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentServiceProxy()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7106/comments/")
            };
        }

        /// <summary>
        /// Adds a new comment.
        /// </summary>
        public Comment AddComment(string content, long userId, long postId)
        {
            var comment = new Comment
            {
                Content = content,
                UserId = userId,
                PostId = postId,
                CreatedDate = DateTime.UtcNow
            };

            var response = _httpClient.PostAsJsonAsync("", comment).Result;
            response.EnsureSuccessStatusCode();

            return response.Content.ReadFromJsonAsync<Comment>().Result!;
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        public void DeleteComment(long commentId)
        {
            var response = _httpClient.DeleteAsync($"{commentId}").Result;
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Retrieves all comments.
        /// </summary>
        public List<Comment> GetAllComments()
        {
            return _httpClient.GetFromJsonAsync<List<Comment>>("").Result!;
        }

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        public Comment GetCommentById(int commentId)
        {
            return _httpClient.GetFromJsonAsync<Comment>($"{commentId}").Result!;
        }

        /// <summary>
        /// Retrieves all comments for a specific post.
        /// </summary>
        public List<Comment> GetCommentsByPostId(long postId)
        {
            return _httpClient.GetFromJsonAsync<List<Comment>>($"post/{postId}").Result!;
        }

        /// <summary>
        /// Updates the content of an existing comment.
        /// </summary>
        public void UpdateComment(long commentId, string content)
        {
            var commentDto = new
            {
                Content = content
            };

            var response = _httpClient.PutAsJsonAsync($"{commentId}", commentDto).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
