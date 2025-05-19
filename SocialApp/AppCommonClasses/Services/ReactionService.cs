﻿namespace SocialApp.Services
{
    using System;
    using System.Collections.Generic;
    using AppCommonClasses.Models;
    using AppCommonClasses.Enums;
    using SocialApp.Repository;
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;

    public class ReactionService(IReactionRepository reactionRepository) : IReactionService
    {

        public Reaction AddReaction(long userId, long postId, ReactionType type)
        {
            if (reactionRepository.GetReactionByUserAndPost(userId, postId) != null)
            {
                if (reactionRepository.GetReactionByUserAndPost(userId, postId).Type == type)
                    reactionRepository.DeleteByUserAndPost(userId, postId);
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
