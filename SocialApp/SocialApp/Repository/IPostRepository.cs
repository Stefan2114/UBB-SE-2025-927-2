using SocialApp.Entities;
using SocialApp.Enums;
using System.Collections.Generic;

namespace SocialApp.Repository
{
    public interface IPostRepository
    {
        void DeletePostById(long postId);
        List<Post> GetAllPosts();
        List<Post> GetPostsByGroupId(long groupId);
        Post GetPostById(long postId);
        List<Post> GetPostsByUserId(long userId);
        List<Post> GetPostsGroupsFeed(long userId);
        List<Post> GetPostsHomeFeed(long userId);
        void SavePost(Post entity);
        void UpdatePostById(long postId, string title, string content, PostVisibility visibility, PostTag tag);
    }
}