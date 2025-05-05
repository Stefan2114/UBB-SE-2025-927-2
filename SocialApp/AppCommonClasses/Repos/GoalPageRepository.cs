namespace AppCommonClasses.Repos
{
    using AppCommonClasses.Data;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository for managing goal-related operations in the database.
    /// </summary>
    public class GoalPageRepository : IGoalPageRepository
    {
        private readonly SocialAppDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoalPageRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used.</param>
        public GoalPageRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Adds a goal and associates it with a user, creating both if they do not already exist.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="goalDescription">The description of the goal.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public void AddGoals(string username, string goalDescription)
        {
            Goal goal = this.dbContext.Goals.First(goal => goal.Description == goalDescription);
            User user = this.dbContext.Users.First(user => user.Username == username);
            user.GoalId = goal.GoalId;
            this.dbContext.SaveChanges();
        }
    }
}