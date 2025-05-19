﻿using AppCommonClasses.Models;
using AppCommonClasses.Enums;
using System.Collections.Generic;

namespace AppCommonClasses.Interfaces
{
    public interface IReactionService
    {
        List<Reaction> GetAllReactions();

        List<Reaction> GetReactionsForPost(long postId);

        Reaction AddReaction(long userId, long postId, ReactionType type);

        void DeleteReaction(long userId, long postId);
    }
}