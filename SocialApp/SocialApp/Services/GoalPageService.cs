namespace SocialApp.Services
{
    using System;
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;

    public class GoalPageService : IGoalPageService
    {
        private IGoalPageRepository goalPageRepository;

        [System.Obsolete]
        public GoalPageService(IGoalPageRepository goalRepo)
        {
            this.goalPageRepository = goalRepo;
        }

        public void AddGoals(string userName, string g_description)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(g_description))
            {
                throw new System.Exception("User name and goal description cannot be empty");
            }
            if (this.goalPageRepository == null)
            {
                throw new System.Exception("Goal repository is not initialized");
            }
            this.goalPageRepository.AddGoals(userName, g_description);
        }

        /*
        [System.Obsolete]
        public void AddGoals(string username, string g_description)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(g_description))
            {
                throw new SystemException("Username and goal description cannot be null or empty.");
            }

            if (this.goalPageRepository == null)
            {
                throw new SystemException("GoalPageRepository is not initialized.");
            }

            parameters = new SqlParameter[]
            {
        new SqlParameter("@u_id", u_id),
        new SqlParameter("@g_id", g_id),
            };

            dataLink.ExecuteNonQuery("UpdateUserGoals", parameters);
        }*/

    }
}