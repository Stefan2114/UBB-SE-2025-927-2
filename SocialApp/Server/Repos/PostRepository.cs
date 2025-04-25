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


    /// <summary>
    /// Repository for managing posts.
    /// </summary>
    public class PostRepository : IPostRepository
    {

        private string loginString = "Data Source=vm;" +
     "Initial Catalog=team_babes;" +
     "Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class.
        /// </summary>
        public PostRepository()
        {
            this.connection = new SqlConnection(this.loginString);
        }

        /// <summary>
        /// Gets all posts from the Database.
        /// </summary>
        /// <returns>Returns a list of all posts.</returns>
        public List<Post> GetAllPosts()
        {
            this.connection.Open();
            List<Post> posts = new List<Post>();

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Posts", this.connection);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Post post = new Post
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    GroupId = reader.IsDBNull(reader.GetOrdinal("GroupId")) ? 0 : reader.GetInt64(reader.GetOrdinal("GroupId")),
                    Visibility = (PostVisibility)reader.GetInt32(reader.GetOrdinal("PostVisibility")),
                    Tag = reader.IsDBNull(reader.GetOrdinal("PostTag")) ? PostTag.Misc : (PostTag)reader.GetInt32(reader.GetOrdinal("PostTag")),
                };
                posts.Add(post);
            }

            reader.Close();
            this.connection.Close();
            return posts;
        }

        /// <summary>
        /// Gets the home feed posts for a user from the Database.
        /// </summary>
        /// <param name="userId">The ID of the user whose home feed to retrieve.</param>
        /// <returns>A list of posts for the user's home feed.</returns>
        public List<Post> GetPostsHomeFeed(long userId)
        {
            this.connection.Open();
            List<Post> posts = new List<Post>();
            SqlCommand selectCommand = new SqlCommand(
                "SELECT * FROM Posts WHERE UserId IN (SELECT UserId FROM UserFollowers WHERE FollowerId = @UserId AND (PostVisibility = 2 OR PostVisibility = 3)) OR UserId = @UserId OR PostVisibility = 3 ORDER BY CreatedDate DESC",
                this.connection);
            if (userId == -1)
            {
                selectCommand = new SqlCommand(
                    "SELECT * FROM Posts WHERE PostVisibility = 3 ORDER BY CreatedDate DESC",
                    this.connection);
            }

            selectCommand.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Post post = new Post
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    GroupId = reader.IsDBNull(reader.GetOrdinal("GroupId")) ? 0 : reader.GetInt64(reader.GetOrdinal("GroupId")),
                    Visibility = (PostVisibility)reader.GetInt32(reader.GetOrdinal("PostVisibility")),
                    Tag = reader.IsDBNull(reader.GetOrdinal("PostTag")) ? PostTag.Misc : (PostTag)reader.GetInt32(reader.GetOrdinal("PostTag")),
                };
                posts.Add(post);
            }

            reader.Close();
            this.connection.Close();
            return posts;
        }

        /// <summary>
        /// Gets the group feed posts for a user from the Database.
        /// </summary>
        /// <param name="userId">The ID of the user whose group feed to retrieve.</param>
        /// <returns>A list of posts for the user's group feed.</returns>
        public List<Post> GetPostsGroupsFeed(long userId)
        {
            this.connection.Open();
            List<Post> posts = new List<Post>();
            SqlCommand selectCommand = new SqlCommand(
                "SELECT * FROM Posts WHERE GroupId IN (SELECT GroupId FROM GroupUsers WHERE UserId = @UserId) ORDER BY CreatedDate DESC",
                this.connection);

            selectCommand.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                Post post = new Post
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    GroupId = reader.GetInt64(reader.GetOrdinal("GroupId")),
                    Visibility = (PostVisibility)reader.GetInt32(reader.GetOrdinal("PostVisibility")),
                    Tag = (PostTag)reader.GetInt32(reader.GetOrdinal("PostTag")),
                };
                posts.Add(post);
            }

            reader.Close();
            this.connection.Close();
            return posts;
        }

        /// <summary>
        /// Gets posts by user ID from the Database.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts to retrieve.</param>
        /// <returns>A list of posts by the specified user.</returns>
        public List<Post> GetPostsByUserId(long userId)
        {
            this.connection.Open();
            List<Post> posts = new List<Post>();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Posts WHERE UserId = @UserId ORDER BY CreatedDate DESC", this.connection);
            selectCommand.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Post post = new Post
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    GroupId = reader.IsDBNull(reader.GetOrdinal("GroupId")) ? 0 : reader.GetInt64(reader.GetOrdinal("GroupId")),
                    Visibility = (PostVisibility)reader.GetInt32(reader.GetOrdinal("PostVisibility")),
                    Tag = reader.IsDBNull(reader.GetOrdinal("PostTag")) ? PostTag.Misc : (PostTag)reader.GetInt32(reader.GetOrdinal("PostTag")),
                };
                posts.Add(post);
            }

            reader.Close();
            this.connection.Close();
            return posts;
        }

        /// <summary>
        /// Gets posts by group ID from the Database.
        /// </summary>
        /// <param name="groupId">The ID of the group whose posts to retrieve.</param>
        /// <returns>A list of posts in the specified group.</returns>
        public List<Post> GetPostsByGroupId(long groupId)
        {
            this.connection.Open();
            List<Post> posts = new List<Post>();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Posts WHERE GroupId = @GroupId ORDER BY CreatedDate DESC", this.connection);
            selectCommand.Parameters.AddWithValue("@GroupId", groupId);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Post post = new Post
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    GroupId = reader.IsDBNull(reader.GetOrdinal("GroupId")) ? 0 : reader.GetInt64(reader.GetOrdinal("GroupId")),
                    Visibility = (PostVisibility)reader.GetInt32(reader.GetOrdinal("PostVisibility")),
                    Tag = reader.IsDBNull(reader.GetOrdinal("PostTag")) ? PostTag.Misc : (PostTag)reader.GetInt32(reader.GetOrdinal("PostTag")),
                };
                posts.Add(post);
            }

            reader.Close();
            this.connection.Close();
            return posts;
        }

        /// <summary>
        /// Gets a post by ID from the Database.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The post with the specified ID.</returns>
        public Post GetPostById(long postId)
        {
            this.connection.Open();
            Post? post = null;

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Posts WHERE Id = @Id ORDER BY CreatedDate DESC", this.connection);
            selectCommand.Parameters.AddWithValue("@Id", postId);

            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                post = new Post
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    GroupId = reader.IsDBNull(reader.GetOrdinal("GroupId")) ? 0 : reader.GetInt64(reader.GetOrdinal("GroupId")),
                    Visibility = (PostVisibility)reader.GetInt32(reader.GetOrdinal("PostVisibility")),
                    Tag = reader.IsDBNull(reader.GetOrdinal("PostTag")) ? PostTag.Misc : (PostTag)reader.GetInt32(reader.GetOrdinal("PostTag")),
                };
            }

            reader.Close();
            this.connection.Close();
            return post;
        }

        /// <summary>
        /// Adds a new post in the Database.
        /// </summary>
        /// <param name="entity">The post that needs to be added.</param>
        public void SavePost(Post entity)
        {
            this.connection.Open();

            SqlCommand insertCommand = new SqlCommand(
                "INSERT INTO Posts (Title, Content, CreatedDate, UserId, PostVisibility, GroupId, PostTag) VALUES (@Title, @Description, @CreatedDate, @UserId, @PostVisibility, @GroupId, @PostTag)",
                this.connection);

            insertCommand.Parameters.AddWithValue("@Title", entity.Title);
            insertCommand.Parameters.AddWithValue("@Description", entity.Content);
            insertCommand.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
            insertCommand.Parameters.AddWithValue("@UserId", entity.UserId);
            insertCommand.Parameters.AddWithValue("@PostVisibility", (int)entity.Visibility);
            insertCommand.Parameters.AddWithValue("@GroupId", entity.GroupId);
            insertCommand.Parameters.AddWithValue("@PostTag", (int)entity.Tag);

            insertCommand.ExecuteNonQuery();

            this.connection.Close();
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
            this.connection.Open();

            SqlCommand updateCommand = new SqlCommand(
                "UPDATE Posts SET Title = @Title, Description = @Description, PostVisibility = @PostVisibility, PostTag = @PostTag WHERE Id = @Id",
                this.connection);

            updateCommand.Parameters.AddWithValue("@Id", postId);
            updateCommand.Parameters.AddWithValue("@Title", title);
            updateCommand.Parameters.AddWithValue("@Description", content);
            updateCommand.Parameters.AddWithValue("@PostVisibility", (int)visibility);
            updateCommand.Parameters.AddWithValue("@PostTag", (int)tag);

            updateCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        /// <summary>
        /// Deletes a post by ID from the Database.
        /// </summary>
        /// <param name="postId">The ID of the post to delete.</param>
        public void DeletePostById(long postId)
        {
            this.connection.Open();

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Posts WHERE Id = @Id", this.connection);
            deleteCommand.Parameters.AddWithValue("@Id", postId);
            deleteCommand.ExecuteNonQuery();

            this.connection.Close();
        }
    }
}
