namespace Server.Interfaces
{
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Defines the contract for goal-related operations.
    /// </summary>
    public interface IGoalController
    {
        /// <summary>  
        /// Adds a new goal for a user.  
        /// </summary>  
        /// <param name="firstName">The first name of the user.</param>  
        /// <param name="lastName">The last name of the user.</param>  
        /// <param name="g_description">The description of the goal.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/>.</returns>  
        IActionResult AddGoals(string name, string g_description);
    }
}
