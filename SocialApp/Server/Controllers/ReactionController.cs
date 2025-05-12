namespace Server.Controllers
{
    using AppCommonClasses.Enums;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    using Server.Interfaces;

    /// <summary>
    /// The controller that manages the reactions.
    /// </summary>
    [ApiController]
    [Route("reactions")]
    public class ReactionController : ControllerBase, IReactionController
    {
        private readonly IReactionRepository reactionRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="ReactionController"/> class.
        /// </summary>
        /// <param name="reactionRepository">The reaction repository that the controller uses.</param>
        public ReactionController(IReactionRepository reactionRepository)
        {
            this.reactionRepository = reactionRepository;
        }

        /// <summary>
        /// Retrieves all reactions.
        /// </summary>
        /// <returns>A list of all reactions.</returns>
        [HttpGet]
        public ActionResult<List<Reaction>> GetAllReactions()
        {
            return this.reactionRepository.GetAllReactions();
        }

        /// <summary>
        /// Retrieves all reactions for a specific post.
        /// </summary>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A list of reactions for the specified post.</returns>
        [HttpGet("post/{postId}")]
        public ActionResult<List<Reaction>> GetReactionsByPost(long postId)
        {
            return this.reactionRepository.GetReactionsByPost(postId);
        }

        /// <summary>
        /// Retrieves a reaction by a specific user for a specific post.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>The reaction for the specified user and post.</returns>
        [HttpGet("{userId}/{postId}")]
        public ActionResult<Reaction> GetReactionByUserAndPost(long userId, long postId)
        {
            return this.reactionRepository.GetReactionByUserAndPost(userId, postId);
        }

        /// <summary>
        /// Saves a new reaction to the repository.
        /// </summary>
        /// <param name="entity">The reaction entity to save.</param>
        [HttpPost]
        public IActionResult SaveReaction(Reaction entity)
        {
            this.reactionRepository.Save(entity);
            return Ok();
        }
        /// <summary>
        /// Updates the reaction type for a specific user and post.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="postId">The ID of the post.</param>
        /// <param name="type">The new reaction type.</param>
        [HttpPut("{userId}/{postId}")]
        public IActionResult UpdateByUserAndPost(long userId, long postId, [FromBody] ReactionDTO reaction)
        {
            this.reactionRepository.UpdateByUserAndPost(userId, postId, reaction.Type);
            return Ok();
        }

        /// <summary>
        /// Deletes a reaction by a specific user for a specific post.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="postId">The ID of the post.</param>
        [HttpDelete("{userId}/{postId}")]
        public IActionResult DeleteByUserAndPost(long userId, long postId)
        {
            this.reactionRepository.DeleteByUserAndPost(userId, postId);
            return Ok();
        }
    }
}