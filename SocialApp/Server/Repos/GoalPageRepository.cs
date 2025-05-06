namespace Server.Repos
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;
    using Server.Data;

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
            this.dbContext = context;
        }

        /// <summary>
        /// Adds a goal and associates it with a user, creating both if they do not already exist.
        /// </summary>
        /// <param name="firstName">The first name of the user.</param>
        /// <param name="lastName">The last name of the user.</param>
        /// <param name="g_description">The description of the goal.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddGoals(string firstName, string lastName, string g_description)
        {
            string name = lastName + " " + firstName;

            Goal? goal = await this.dbContext.Goals.FirstOrDefaultAsync(g => g.Description == g_description);

            if (goal == null)
            {
                goal = new Goal(0, g_description);
                this.dbContext.Goals.Add(goal);
                await this.dbContext.SaveChangesAsync();
            }

            User? user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.Username == name);

            if (user == null)
            {
                user = new User
                {
                    Username = name,
                    GoalId = goal.GoalId,
                };
                this.dbContext.Users.Add(user);
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
