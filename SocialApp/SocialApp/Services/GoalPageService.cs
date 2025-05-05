namespace SocialApp.Services
{
    using System;
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;

    public class GoalPageService : IGoalPageService
    {
        private IGoalPageRepository goalPageRepository;

        [System.Obsolete]
        public GoalPageService(IGoalPageRepository goalPageRepository)
        {
            this.goalPageRepository = goalPageRepository;
        }

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

            this.goalPageRepository.AddGoals(username, g_description);
        }
    }
}