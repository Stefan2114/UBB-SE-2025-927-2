using System.Collections.Generic;
using System.Linq;
using AppCommonClasses.Models;
using AppCommonClasses.Enums;
using Server.Data;
using AppCommonClasses.Interfaces;

namespace Server.Repos
{
    /// <summary>
    /// Repository for managing posts in the database.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private readonly SocialAppDbContext dbContext;

        public PostRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }

        public Post GetPostById(long postId)
        {
            return this.dbContext.Posts.FirstOrDefault(p => p.Id == postId);
        }

        public List<Post> GetAllPosts()
        {
            return this.dbContext.Posts.ToList();
        }

        public void SavePost(Post entity)
        {
            this.dbContext.Posts.Add(entity);
            this.dbContext.SaveChanges();
        }

        public bool UpdatePostById(long postId, string title, string content, PostVisibility visibility, PostTag tag)
        {
            var post = this.dbContext.Posts.Find(postId);
            if (post != null)
            {
                post.Title = title;
                post.Content = content;
                post.Visibility = visibility;
                post.Tag = tag;
                this.dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePostById(long postId)
        {
            var post = this.dbContext.Posts.Find(postId);
            if (post != null)
            {
                this.dbContext.Posts.Remove(post);
                this.dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Post> GetPostsHomeFeed(long userId)
        {
            var postsQuery = from post in this.dbContext.Posts
                             where post.UserId == userId
                                || post.Visibility == PostVisibility.Public
                                || this.dbContext.UserFollowers.Any(userFollower =>
                                       userFollower.FollowerId == userId
                                    && userFollower.UserId == post.UserId
                                    && (post.Visibility == PostVisibility.Friends || post.Visibility == PostVisibility.Groups))
                             orderby post.CreatedDate descending
                             select post;

            return postsQuery.ToList();
        }

        public List<Post> GetPostsGroupsFeed(long userId)
        {
            var postsQuery = from post in this.dbContext.Posts
                             where this.dbContext.GroupUsers.Any(groupUser =>
                                   groupUser.UserId == userId && groupUser.GroupId == post.GroupId)
                             orderby post.CreatedDate descending
                             select post;

            return postsQuery.ToList();
        }

        public List<Post> GetPostsByUserId(long userId)
        {
            return this.dbContext.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedDate)
                .ToList();
        }

        public List<Post> GetPostsByGroupId(long groupId)
        {
            return this.dbContext.Posts
                .Where(p => p.GroupId == groupId)
                .OrderByDescending(p => p.CreatedDate)
                .ToList();
        }

    }
}
