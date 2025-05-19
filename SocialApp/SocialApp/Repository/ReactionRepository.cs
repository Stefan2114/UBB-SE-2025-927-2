using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AppCommonClasses.Enums;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

namespace SocialApp.Repository
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly IDataLink dataLink;

        public ReactionRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        [Obsolete]
        public ReactionRepository()
        {
            // this.dataLink = new DataLink();
        }

        public async Task<List<Reaction>> GetAllReactionsAsync()
        {
            string query = "SELECT * FROM Reactions";
            var dataTable = this.dataLink.ExecuteSqlQuery(query, null);

            var reactions = new List<Reaction>();
            foreach (DataRow row in dataTable.Rows)
            {
                reactions.Add(new Reaction
                {
                    UserId = Convert.ToInt64(row["UserId"]),
                    PostId = Convert.ToInt64(row["PostId"]),
                    Type = (ReactionType)Convert.ToInt32(row["ReactionType"])
                });
            }

            return await Task.FromResult(reactions);
        }

        public async Task<List<Reaction>> GetReactionsByPostAsync(long postId)
        {
            string query = "SELECT * FROM Reactions WHERE PostId = @PostId";
            var parameters = new SqlParameter[]
            {
                new("@PostId", postId)
            };

            var dataTable = this.dataLink.ExecuteSqlQuery(query, parameters);

            var reactions = new List<Reaction>();
            foreach (DataRow row in dataTable.Rows)
            {
                reactions.Add(new Reaction
                {
                    UserId = Convert.ToInt64(row["UserId"]),
                    PostId = Convert.ToInt64(row["PostId"]),
                    Type = (ReactionType)Convert.ToInt32(row["ReactionType"])
                });
            }

            return await Task.FromResult(reactions);
        }

        public async Task<Reaction?> GetReactionByUserAndPostAsync(long userId, long postId)
        {
            string query = "SELECT * FROM Reactions WHERE UserId = @UserId AND PostId = @PostId";
            var parameters = new SqlParameter[]
            {
                new("@UserId", userId),
                new("@PostId", postId)
            };

            var table = this.dataLink.ExecuteSqlQuery(query, parameters);

            if (table.Rows.Count == 0)
                return null;

            var row = table.Rows[0];
            var reaction = new Reaction
            {
                UserId = Convert.ToInt64(row["UserId"]),
                PostId = Convert.ToInt64(row["PostId"]),
                Type = (ReactionType)Convert.ToInt32(row["ReactionType"])
            };

            return await Task.FromResult(reaction);
        }

        public async Task SaveAsync(Reaction reaction)
        {
            string query = @"INSERT INTO Reactions (UserId, PostId, ReactionType) 
                             VALUES (@UserId, @PostId, @ReactionType)";
            var parameters = new SqlParameter[]
            {
                new("@UserId", reaction.UserId),
                new("@PostId", reaction.PostId),
                new("@ReactionType", (int)reaction.Type)
            };

            this.dataLink.ExecuteNonQuery(query, parameters);
            await Task.CompletedTask;
        }

        public async Task UpdateByUserAndPostAsync(long userId, long postId, ReactionType type)
        {
            string query = @"UPDATE Reactions 
                             SET ReactionType = @Type 
                             WHERE UserId = @UserId AND PostId = @PostId";
            var parameters = new SqlParameter[]
            {
                new("@UserId", userId),
                new("@PostId", postId),
                new("@Type", (int)type)
            };

            this.dataLink.ExecuteNonQuery(query, parameters);
            await Task.CompletedTask;
        }

        public async Task DeleteByUserAndPostAsync(long userId, long postId)
        {
            string query = "DELETE FROM Reactions WHERE UserId = @UserId AND PostId = @PostId";
            var parameters = new SqlParameter[]
            {
                new("@UserId", userId),
                new("@PostId", postId)
            };

            this.dataLink.ExecuteNonQuery(query, parameters);
            await Task.CompletedTask;
        }
    }
}
