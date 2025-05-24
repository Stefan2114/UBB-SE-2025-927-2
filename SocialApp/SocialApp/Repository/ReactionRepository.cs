namespace SocialApp.Repository
{
    using System.Collections.Generic;
    using AppCommonClasses.Enums;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.Data.SqlClient;

    public class ReactionRepository : IReactionRepository
    {


        private string loginString = "Server=CATALIN\\SQLEXPRESS;Database=MealSocialApp;Trusted_Connection=True;TrustServerCertificate=True;";

        private SqlConnection connection;

        public ReactionRepository()
        {
            this.connection = new SqlConnection(this.loginString);
        }

        public List<Reaction> GetAllReactions()
        {
            this.connection.Open();
            List<Reaction> reactions = [];

            SqlCommand selectCommand = new("SELECT * FROM Reactions", this.connection);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Reaction reaction = new()
                {
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    PostId = reader.GetInt64(reader.GetOrdinal("PostId")),
                    Type = (ReactionType)reader.GetInt32(reader.GetOrdinal("Type")),
                };
                reactions.Add(reaction);
            }

            reader.Close();
            this.connection.Close();
            return reactions;
        }

        public List<Reaction> GetReactionsByPost(long postId)
        {
            this.connection.Open();

            List<Reaction> reactions = [];
            SqlCommand selectCommand = new(
                "SELECT * FROM Reactions WHERE PostId = @PostId",
                this.connection);
            selectCommand.Parameters.AddWithValue("@PostId", postId);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Reaction reaction = new()
                {
                    UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt64(reader.GetOrdinal("UserId")),
                    PostId = reader.IsDBNull(reader.GetOrdinal("PostId")) ? 0 : reader.GetInt64(reader.GetOrdinal("PostId")),
                    Type = reader.IsDBNull(reader.GetOrdinal("ReactionType")) ? ReactionType.Like : (ReactionType)reader.GetInt32(reader.GetOrdinal("ReactionType")),
                };
                reactions.Add(reaction);
            }

            reader.Close();
            this.connection.Close();
            return reactions;
        }

        public Reaction? GetReactionByUserAndPost(long userId, long postId)
        {
            this.connection.Open();
            Reaction? reaction = null;

            SqlCommand selectCommand = new("SELECT * FROM Reactions WHERE UserId = @UserId AND PostId = @PostId", this.connection);
            selectCommand.Parameters.AddWithValue("@UserId", userId);
            selectCommand.Parameters.AddWithValue("@PostId", postId);

            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                reaction = new Reaction
                {
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    PostId = reader.GetInt64(reader.GetOrdinal("PostId")),
                    Type = (ReactionType)reader.GetInt32(reader.GetOrdinal("ReactionType")),
                };
            }

            reader.Close();
            this.connection.Close();
            return reaction;
        }

        public void Save(Reaction entity)
        {
            this.connection.Open();

            SqlCommand insertCommand = new(
                "INSERT INTO Reactions (UserId, PostId, ReactionType) VALUES (@UserId, @PostId, @Type)",
                this.connection);
            insertCommand.Parameters.AddWithValue("@UserId", entity.UserId);
            insertCommand.Parameters.AddWithValue("@PostId", entity.PostId);
            insertCommand.Parameters.AddWithValue("@Type", (int)entity.Type);

            insertCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        public void UpdateByUserAndPost(long userId, long postId, ReactionType type)
        {
            this.connection.Open();

            SqlCommand updateCommand = new(
                "UPDATE Reactions SET ReactionType = @Type WHERE UserId = @UserId AND PostId = @PostId",
                this.connection);

            updateCommand.Parameters.AddWithValue("@UserId", userId);
            updateCommand.Parameters.AddWithValue("@PostId", postId);
            updateCommand.Parameters.AddWithValue("@Type", (int)type);
            updateCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        public void DeleteByUserAndPost(long userId, long postId)
        {
            this.connection.Open();

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Reactions WHERE UserId = @UserId AND PostId = @PostId", this.connection);
            deleteCommand.Parameters.AddWithValue("@UserId", userId);
            deleteCommand.Parameters.AddWithValue("@PostId", postId);
            deleteCommand.ExecuteNonQuery();

            this.connection.Close();
        }
    }
}