namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Exceptions;
    using SocialApp.Interfaces;
    using SocialApp.Queries;
    using System.Data.SqlClient;
    using System.Diagnostics;

    public class GoalPageService : IGoalPageService
    {
        private readonly IDataLink dataLink;

        [System.Obsolete]
        public GoalPageService(IDataLink? dataLink = null)
        {
            this.dataLink = dataLink ?? DataLink.Instance; // Default to singleton if none provided
        }

        [System.Obsolete]
        public void AddGoals(string username, string g_description)
        {
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@u_name", username),
            };

            Debug.WriteLine($"User name: {username}");

            int u_id = dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            // Check if the user ID is valid
            if (u_id == 0)
            {
                throw new DatabaseOperationException($"User {username} not found.");
            }

            Debug.WriteLine($"User ID: {u_id}");

            parameters = new SqlParameter[]
            {
        new SqlParameter("@g_description", g_description),
            };

            int g_id = dataLink.ExecuteScalar<int>("SELECT dbo.GetGoalByDescription(@g_description)", parameters, false);

            // Check if the goal ID is valid
            if (g_id == 0)
            {
                throw new DatabaseOperationException($"Goal {g_description} not found.");
            }

            parameters = new SqlParameter[]
            {
        new SqlParameter("@u_id", u_id),
        new SqlParameter("@g_id", g_id),
            };

            dataLink.ExecuteNonQuery("UpdateUserGoals", parameters);
        }

    }
}