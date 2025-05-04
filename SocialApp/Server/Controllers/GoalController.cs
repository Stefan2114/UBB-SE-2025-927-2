namespace Server.Controllers
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
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
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="g_description">The description of the goal.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost]
        public IActionResult AddGoals(string firstName, string lastName, string g_description)
        {
            this.goalRepository.AddGoals(firstName, lastName, g_description);
            return this.Ok();
        }
    }
}