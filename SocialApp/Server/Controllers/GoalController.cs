namespace Server.Controllers
{
    using AppCommonClasses.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Server.DTOs;
    using Server.Interfaces;

    /// <summary>
    /// Controller for managing goals.
    /// </summary>
    [ApiController]
    [Route("goals")]
    public class GoalController : ControllerBase, IGoalController
    {
        private readonly IGoalPageRepository goalRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoalController"/> class.
        /// </summary>
        /// <param name="goalRepository">The goal repository instance.</param>
        public GoalController(IGoalPageRepository goalRepository)
        {
            this.goalRepository = goalRepository;
        }

        /// <summary>
        /// Adds a new goal.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="g_description">The description of the goal.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        public IActionResult AddGoals([FromBody] GoalDTO goal)
        {
            this.goalRepository.AddGoals(goal.Username, goal.Description);
            return this.Ok();
        }
    }
}