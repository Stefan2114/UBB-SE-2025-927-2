namespace SocialApp.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using AppCommonClasses.Interfaces;

    /// <summary>  
    /// Proxy class for interacting with the Goal repository API.  
    /// </summary>  
    public class GoalRepositoryProxy : IGoalPageRepository
    {
        private readonly HttpClient httpClient;

        /// <summary>  
        /// Initializes a new instance of the <see cref="GoalRepositoryProxy"/> class.  
        /// Sets up the HttpClient with the base address for the Goal API.  
        /// </summary>  
        public GoalRepositoryProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/goals/");
        }

        /// <summary>  
        /// Adds a new goal to the repository.  
        /// </summary>  
        /// <param name="firstName">The first name of the user associated with the goal.</param>  
        /// <param name="lastName">The last name of the user associated with the goal.</param>  
        /// <param name="g_description">The description of the goal.</param>  
        /// <returns>A task representing the asynchronous operation.</returns>  
        public Task AddGoals(string firstName, string lastName, string g_description)
        {
            var goal = new { FirstName = firstName, LastName = lastName, Description = g_description };
            return this.httpClient.PostAsJsonAsync(string.Empty, goal);
        }
    }
}
