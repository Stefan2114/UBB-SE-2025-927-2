namespace Server.Repos
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using AppCommonClasses.Models;
    using AppCommonClasses.Enums;
    using AppCommonClasses.Repos;
    using Server.Data;


    /// <summary>
    /// Repository for managing posts.
    /// </summary>
    public class PostRepository : IPostRepository
    {

        private readonly SocialAppDbContext dbContext;


        public PostRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }


        /// <summary>
        /// Gets a post by ID from the Database.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The post with the specified ID.</returns>
        public Post GetPostById(long postId)
        {
            return this.dbContext.Posts.First(p => p.Id == postId);
        }

        /// <summary>
        /// Gets all posts from the Database.
        /// </summary>
        /// <returns>Returns a list of all posts.</returns>
        public List<Post> GetAllPosts()
        {
            return this.dbContext.Posts.ToList();
        }


        /// <summary>
        /// Adds a new post in the Database.
        /// </summary>
        /// <param name="entity">The post that needs to be added.</param>
        public void SavePost(Post entity)
        {
            this.dbContext.Posts.Add(entity);
            this.dbContext.SaveChanges();
        }


        /// <summary>
        /// Updates a post by ID from the Database.
        /// </summary>
        /// <param name="postId">The ID of the post to update.</param>
        /// <param name="title">The new title of the post.</param>
        /// <param name="content">The new description of the post.</param>
        /// <param name="visibility">The new visibility of the post.</param>
        /// <param name="tag">The new tag of the post.</param>
        public void UpdatePostById(long postId, string title, string content, PostVisibility visibility, PostTag tag)
        {
            var post = this.dbContext.Posts.Find(postId);
            if(post != null)
            {
                post.Title = title;
                post.Content = content;
                post.Visibility = visibility;
                post.Tag = tag;
                this.dbContext.SaveChanges();
            }
        }


        /// <summary>
        /// Deletes a post by ID from the Database.
        /// </summary>
        /// <param name="postId">The ID of the post to delete.</param>
        public void DeletePostById(long postId)
        {
            var post = this.dbContext.Posts.Find(postId);
            if (post != null)
            {
                this.dbContext.Posts.Remove(post);
                this.dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the home feed posts for a user from the Database.
        /// </summary>
        /// <param name="userId">The ID of the user whose home feed to retrieve.</param>
        /// <returns>A list of posts for the user's home feed.</returns>
        public List<Post> GetPostsHomeFeed(long userId)
        {

            var postsQuery = from post in this.dbContext.Posts
                             where post.UserId == userId || post.Visibility == PostVisibility.Public || 
                             this.dbContext.UserFollowers.Any(userFollower => userFollower.FollowerId == userId && userFollower.UserId == post.UserId && (post.Visibility == PostVisibility.Friends || post.Visibility == PostVisibility.Groups))
                             orderby post.CreatedDate descending
                             select post;

            System.Diagnostics.Debug.WriteLine("Fetching home feed for user was a success: " + userId);


            return postsQuery.ToList();


        }

        /// <summary>
        /// Gets the group feed posts for a user from the Database.
        /// </summary>
        /// <param name="userId">The ID of the user whose group feed to retrieve.</param>
        /// <returns>A list of posts for the user's group feed.</returns>
        public List<Post> GetPostsGroupsFeed(long userId)
        {
            var postsQuery = from post in this.dbContext.Posts
                             where this.dbContext.GroupUsers.Any(groupUser => groupUser.UserId == userId && groupUser.GroupId == post.GroupId)
                             orderby post.CreatedDate descending
                             select post;


            return postsQuery.ToList();

        }

        /// <summary>
        /// Gets posts by user ID from the Database.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts to retrieve.</param>
        /// <returns>A list of posts by the specified user.</returns>
        public List<Post> GetPostsByUserId(long userId)
        {
            var postsQuery = from post in this.dbContext.Posts
                             where post.UserId == userId
                             orderby post.CreatedDate descending
                             select post;

            return postsQuery.ToList();
        }

        /// <summary>
        /// Gets posts by group ID from the Database.
        /// </summary>
        /// <param name="groupId">The ID of the group whose posts to retrieve.</param>
        /// <returns>A list of posts in the specified group.</returns>
        public List<Post> GetPostsByGroupId(long groupId)
        {
            var postsQuery = from post in this.dbContext.Posts
                             where post.GroupId == groupId
                             orderby post.CreatedDate descending
                             select post;

            return postsQuery.ToList();
        }

    }
}
