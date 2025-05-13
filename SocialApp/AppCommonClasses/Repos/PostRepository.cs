using AppCommonClasses.Data;
using AppCommonClasses.Enums;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

namespace AppCommonClasses.Repos
{
    /// <summary>
    /// Repository for managing posts in the database.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        private readonly SocialAppDbContext dbContext;

        public PostRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        public Post GetPostById(long postId)
        {
            return dbContext.Posts.FirstOrDefault(p => p.Id == postId);
        }

        public List<Post> GetAllPosts()
        {
            return dbContext.Posts.ToList();
        }

        public void SavePost(Post entity)
        {
            dbContext.Posts.Add(entity);
            dbContext.SaveChanges();
        }

        public bool UpdatePostById(long postId, string title, string content, PostVisibility visibility, PostTag tag)
        {
            var post = dbContext.Posts.Find(postId);
            if (post != null)
            {
                post.Title = title;
                post.Content = content;
                post.Visibility = visibility;
                post.Tag = tag;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePostById(long postId)
        {
            var post = dbContext.Posts.Find(postId);
            if (post != null)
            {
                dbContext.Posts.Remove(post);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Post> GetPostsHomeFeed(long userId)
        {
            var postsQuery = from post in dbContext.Posts
                             where post.UserId == userId
                                || post.Visibility == PostVisibility.Public
                                || dbContext.UserFollowers.Any(userFollower =>
                                       userFollower.FollowerId == userId
                                    && userFollower.UserId == post.UserId
                                    && (post.Visibility == PostVisibility.Friends || post.Visibility == PostVisibility.Groups))
                             orderby post.CreatedDate descending
                             select post;

            return postsQuery.ToList();
        }

        public List<Post> GetPostsGroupsFeed(long userId)
        {
            var postsQuery = from post in dbContext.Posts
                             where dbContext.GroupUsers.Any(groupUser =>
                                   groupUser.UserId == userId && groupUser.GroupId == post.GroupId)
                             orderby post.CreatedDate descending
                             select post;

            return postsQuery.ToList();
        }

        public List<Post> GetPostsByUserId(long userId)
        {
            return dbContext.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedDate)
                .ToList();
        }

        public List<Post> GetPostsByGroupId(long groupId)
        {
            return dbContext.Posts
                .Where(p => p.GroupId == groupId)
                .OrderByDescending(p => p.CreatedDate)
                .ToList();
        }

    }
}
