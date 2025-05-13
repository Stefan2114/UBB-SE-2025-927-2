

namespace SocialApp.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Server.DTOs;
    public class CommentRepositoryProxy : ICommentRepository
    {
        private readonly HttpClient httpClient;

        public CommentRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/comments/");
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        public void DeleteCommentById(long commentId)
        {
            this.httpClient.DeleteAsync($"{commentId}").Wait();
        }

        /// <summary>
        /// Retrieves all comments.
        /// </summary>
        public List<Comment> GetAllComments()
        {
            return this.httpClient.GetFromJsonAsync<List<Comment>>("").Result!;
        }

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        public Comment? GetCommentById(long id)
        {
            return this.httpClient.GetFromJsonAsync<Comment>($"{id}").Result;
        }

        /// <summary>
        /// Retrieves all comments for a specific post.
        /// </summary>
        public List<Comment> GetCommentsByPostId(long postId)
        {
            return this.httpClient.GetFromJsonAsync<List<Comment>>($"post/{postId}").Result!;
        }

        /// <summary>
        /// Saves a new comment.
        /// </summary>
        public void SaveComment(Comment entity)
        {
            this.httpClient.PostAsJsonAsync("", entity).Wait();
        }

        /// <summary>
        /// Updates the content of an existing comment.
        /// </summary>
        public void UpdateCommentContentById(long id, string content)
        {
            Comment comment = this.httpClient.GetFromJsonAsync<Comment>($"{id}").Result!;
            if (comment == null)
            {
                throw new Exception("Comment does not exist");
            }

            var commentDto = new CommentDTO
            {
                Id = id,
                Content = content,
                UserId = comment.UserId,
                PostId = comment.PostId,
                CreatedDate = comment.CreatedDate,
            };

            this.httpClient.PutAsJsonAsync($"{id}", commentDto).Wait();
        }
    }
}
