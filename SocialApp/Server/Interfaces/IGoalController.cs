namespace Server.Interfaces
{
    using Microsoft.AspNetCore.Mvc;
    using Server.DTOs;

    /// <summary>
    /// Defines the contract for goal-related operations.
    /// </summary>
    public interface IGoalController
    {
        /// <summary>
        /// Adds a new goal for a user.
        /// </summary>
        /// <param name="goal">The goal.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/>.</returns>  
        IActionResult AddGoals(GoalDTO goal);
    }
}
