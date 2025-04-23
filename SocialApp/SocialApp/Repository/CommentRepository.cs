using AppCommonClasses.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using AppCommonClasses.Repos;

namespace SocialApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private string loginString = "Data Source=vm;" +
            "Initial Catalog=team_babes;" +
            "Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        public CommentRepository()
        {
            this.connection = new SqlConnection(this.loginString);
        }

        /// <summary>
        /// Retrieves all comments from the database
        /// </summary>
        /// <returns>A list of all Comment entities in the system.</returns>
        public List<Comment> GetAllComments()
        {
            SqlConnection connection1 = this.connection;
            connection1.Open();
            List<Comment> ans = new List<Comment>();

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Comments", this.connection);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Comment comment = new Comment
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    PostId = reader.GetInt64(reader.GetOrdinal("PostId")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                };
                ans.Add(comment);
            }

            reader.Close();
            this.connection.Close();
            return ans;
        }

        /// <summary>
        /// Retrieves all comments associated with a specific post
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve comments for.</param>
        /// <returns>A list of Comment entities for the specified post.</returns>
        public List<Comment> GetCommentsByPostId(long postId)
        {
            this.connection.Open();
            List<Comment> ans = new List<Comment>();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Comments WHERE PostId = @PostId", this.connection);
            selectCommand.Parameters.AddWithValue("@PostId", postId);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Comment comment = new Comment
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    PostId = reader.GetInt64(reader.GetOrdinal("PostId")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                };
                ans.Add(comment);
            }

            reader.Close();
            this.connection.Close();
            return ans;
        }

        /// <summary>
        /// Deletes a comment from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        public void DeleteCommentById(long id)
        {
            this.connection.Open();

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Comments WHERE Id = @Id", this.connection);
            deleteCommand.Parameters.AddWithValue("@Id", id);
            deleteCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        /// <summary>
        /// Retrieves a single comment by its ID
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>The Comment entity with the specified ID, or null if not found.</returns>
        public Comment? GetCommentById(long id) // Updated return type to Comment?
        {
            this.connection.Open();
            Comment? comment = null;

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Comments WHERE Id = @Id", this.connection);
            selectCommand.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                comment = new Comment
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    PostId = reader.GetInt64(reader.GetOrdinal("PostId")),
                    Content = reader.GetString(reader.GetOrdinal("Content")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                };
            }

            reader.Close();
            this.connection.Close();
            return comment;
        }

        /// <summary>
        /// Saves a new comment to the database
        /// </summary>
        /// <param name="entity">The Comment entity to be saved.</param>
        public void SaveComment(Comment entity)
        {
            this.connection.Open();

            SqlCommand insertCommand = new SqlCommand(
                "INSERT INTO Comments (UserId, PostId, Content, CreatedDate) VALUES (@UserId, @PostId, @Content, @CreatedDate)",
                this.connection
            );
            insertCommand.Parameters.AddWithValue("@UserId", entity.UserId);
            insertCommand.Parameters.AddWithValue("@PostId", entity.PostId);
            insertCommand.Parameters.AddWithValue("@Content", entity.Content);
            insertCommand.Parameters.AddWithValue("@CreatedDate", entity.CreatedDate);
            insertCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        /// <summary>
        /// Updates the content of an existing comment.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="content">The new content for the comment.</param>
        public void UpdateCommentContentById(long id, string content)
        {
            this.connection.Open();

            SqlCommand updateCommand = new SqlCommand(
                "UPDATE Comments SET Content = @Content WHERE Id = @Id",
                this.connection
            );

            updateCommand.Parameters.AddWithValue("@Id", id);
            updateCommand.Parameters.AddWithValue("@Content", content);
            updateCommand.ExecuteNonQuery();

            this.connection.Close();
        }
    }
}