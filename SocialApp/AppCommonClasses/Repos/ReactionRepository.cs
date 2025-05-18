namespace AppCommonClasses.Repos
{
    using System.Collections.Generic;
    using System.Linq;
    using AppCommonClasses.Data;
    using AppCommonClasses.Enums;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;


    /// <summary>
    /// Repository for managing reactions.
    /// </summary>
    public class ReactionRepository : IReactionRepository
    {
        private readonly SocialAppDbContext dbContext;

        public ReactionRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Deletes a reaction by a specific user for a specific post.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="postId">The ID of the post.</param>
        public void DeleteByUserAndPost(long userId, long postId)
        {
            var reactionDeleted = (from reaction in dbContext.Reactions
                            where reaction.PostId == postId && reaction.UserId == userId
                            select reaction).FirstOrDefault();
            if (reactionDeleted != null)
            {
                dbContext.Remove(reactionDeleted);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all reactions.
        /// </summary>
        /// <returns>A list of all reactions.</returns>
        public List<Reaction> GetAllReactions()
        {
            return dbContext.Reactions.ToList();
        }

        /// <summary>
        /// Retrieves all reactions for a specific post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A list of reactions for the specified post.</returns>
        public List<Reaction> GetReactionsByPost(long postId)
        {
            var reactionsQuery = from reaction in dbContext.Reactions
                                 where reaction.PostId == postId
                                 select reaction;

            return reactionsQuery.ToList();
        }

        /// <summary>
        /// Retrieves a reaction by a specific user for a specific post.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>The reaction for the specified user and post.</returns>
        public Reaction GetReactionByUserAndPost(long userId, long postId)
        {
            var reactionReturned = (from reaction in dbContext.Reactions
                                    where reaction.PostId == postId && reaction.UserId == userId
                                    select reaction).FirstOrDefault();
            return reactionReturned;
        }

        /// <summary>
        /// Saves a new reaction to the repository.
        /// </summary>
        /// <param name="entity">The reaction entity to save.</param>
        public void Save(Reaction entity)
        {
            dbContext.Reactions.Add(entity);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates the reaction type for a specific user and post.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="postId">The ID of the post.</param>
        /// <param name="type">The new reaction type.</param>
        public void UpdateByUserAndPost(long userId, long postId, ReactionType type)
        {
            var reaction = dbContext.Reactions
                .FirstOrDefault(r => r.PostId == postId && r.UserId == userId);

            if (reaction != null)
            {
                reaction.Type = type;
                dbContext.SaveChanges();
            }
        }
    }
}