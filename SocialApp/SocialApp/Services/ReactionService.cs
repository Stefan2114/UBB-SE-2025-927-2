namespace SocialApp.Services
{
    using System;
    using System.Collections.Generic;
    using SocialApp.Entities;
    using SocialApp.Enums;
    using SocialApp.Repository;

    public class ReactionService(IReactionRepository reactionRepository) : IReactionService
    {
        public Reaction AddReaction(long userId, long postId, ReactionType type)
        {
            if (reactionRepository.GetReactionByUserAndPost(userId, postId) != null)
            {
                reactionRepository.UpdateByUserAndPost(userId, postId, type);
                return reactionRepository.GetReactionByUserAndPost(userId, postId);
            }

            Reaction reaction = new Reaction() { UserId = userId, PostId = postId, Type = type };
            reactionRepository.Save(reaction);
            return reaction;
        }

        public void DeleteReaction(long userId, long postId)
        {
            Reaction reaction = reactionRepository.GetReactionByUserAndPost(userId, postId);
            if (reaction == null)
            {
                throw new Exception("Reaction does not exist");
            }

            reactionRepository.DeleteByUserAndPost(userId, postId);
        }

        public List<Reaction> GetAllReactions()
        {
            return reactionRepository.GetAllReactions();
        }

        public List<Reaction> GetReactionsForPost(long postId)
        {
            return reactionRepository.GetReactionsByPost(postId);
        }
    }
}
