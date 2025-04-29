namespace SocialApp.ViewModels
{
    using AppCommonClasses.Enums;
    using AppCommonClasses.Models;
    using AppCommonClasses.Interfaces;
    using SocialApp.Components;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using SocialApp.Interfaces;
    using global::SocialApp.Components;
    using global::SocialApp.Interfaces;
    using global::SocialApp.Services;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class PostViewModel
    {
        ObservableCollection<PostComponent> posts;
        private IPostService postService;
        
        public PostViewModel(IPostService postService)
        {
            this.postService = postService;
            this.posts = new ObservableCollection<PostComponent>();
        }


        public ObservableCollection<PostComponent> GetCurrentPosts()
        {
            return this.posts;
        }

        public void clearPosts()
        {
            this.posts = new ObservableCollection<PostComponent>();
        }

        private void PopulatePosts(List<Post> receivedPosts)
        {
            foreach (Post post in receivedPosts)
            {
                var postComponent = new PostComponent(post.Title, post.Visibility, post.UserId, post.Content, post.CreatedDate, post.Tag, post.Id);
                this.posts.Add(postComponent);
            }
        }


        public void AddPost(string title, string? content, long userId, long groupId, PostVisibility postVisibility, PostTag postTag)
        {
            this.postService.AddPost(title, content, userId, groupId, postVisibility, postTag);
        }

        /// <summary>
        /// Deletes a post by ID.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        public void DeletePost(long id)
        {
            this.postService.DeletePost(id);
        }

        /// <summary>
        /// Updates a post by ID.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="title">The new title of the post.</param>
        /// <param name="description">The new description of the post.</param>
        /// <param name="visibility">The new visibility of the post.</param>
        /// <param name="tag">The new tag of the post.</param>
        public void UpdatePost(long id, string title, string description, PostVisibility visibility, PostTag tag)
        {
            this.postService.UpdatePost(id, title, description, visibility, tag);
        }

        /// <summary>
        /// Gets all posts.
        /// </summary>
        /// <returns>A list of all posts.</returns>
        public void GetAllPosts()
        {
            var posts = this.postService.GetAllPosts();
            this.PopulatePosts(posts);
        }

        /// <summary>
        /// Gets a post by ID.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>The post with the specified ID.</returns>
        public Post GetPostById(long id)
        {
            return this.postService.GetPostById(id);
        }

        /// <summary>
        /// Gets posts by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts to retrieve.</param>
        /// <returns>A list of posts by the specified user.</returns>
        public void PopulatePostsByUserId(long userId)
        {
            var posts = this.postService.GetPostsByUserId(userId);
        }

        public void PopulatePostsByGroupId(long groupId)
        {
            var posts = this.postService.GetPostsByGroupId(groupId);
            this.PopulatePosts(posts);
        }


        /// <summary>
        /// Gets the group feed posts for a user.
        /// </summary>
        /// <param name="userId">The ID of the user whose group feed to retrieve.</param>
        /// <returns>A list of posts for the user's group feed.</returns>
        public void PopulatePostsGroupsFeed(long userId)
        {
            var posts = this.postService.GetPostsGroupsFeed(userId);
        }

        /// <summary>
        /// Gets the home feed posts for a user.
        /// </summary>
        /// <param name="userId">The ID of the user whose home feed to retrieve.</param>
        /// <returns>A list of posts for the user's home feed.</returns>
        public void PopulatePostsHomeFeed(long userId)
        {
            var posts = this.postService.GetPostsHomeFeed(userId);
            this.PopulatePosts(posts);
        }
    }

}
