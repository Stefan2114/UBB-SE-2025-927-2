using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface IReactionController
    {
        ActionResult<List<Reaction>> GetAllReactions();

        ActionResult<List<Reaction>> GetReactionsByPost(long postId);

        ActionResult<Reaction> GetReactionByUserAndPost(long userId, long postId);

        IActionResult SaveReaction(Reaction entity);

        IActionResult UpdateByUserAndPost(long userId, long postId, [FromBody] ReactionDTO reaction);

        IActionResult DeleteByUserAndPost(long userId, long postId);
    }
}