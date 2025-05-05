namespace Server.Repos
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AppCommonClasses.DTOs;
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
        /// <param name="name">The name of the user.</param>
        /// <param name="g_description">The description of the goal.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public void AddGoals(string name, string g_description)
        {
            Goal goal = this.dbContext.Goals.First(g => g.Description == g_description);
            UserModel user = this.dbContext.Users.First(u => u.Name == name);
            user.GoalId = goal.GoalId;
            this.dbContext.SaveChanges();
        }
    }
}
