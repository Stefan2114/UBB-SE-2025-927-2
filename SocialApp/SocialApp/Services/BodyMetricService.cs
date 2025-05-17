namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;
    using System;
    using System.Linq;

    public class BodyMetricService : IBodyMetricService
    {
        private readonly IBodyMetricRepository bodyMetricRepository;
        private readonly IUserService userService;

        public BodyMetricService(IBodyMetricRepository bodyMetricRepository, IUserService userService)
        {
            this.bodyMetricRepository = bodyMetricRepository;
            this.userService = userService;
        }

        public void UpdateUserBodyMetrics(string  username, string weight, string height, string targetGoal)
        {
            float userWeight = float.Parse(weight);
            float userHeight = float.Parse(height);
            float? userTargetGoal = string.IsNullOrWhiteSpace(targetGoal) ? null : float.Parse(targetGoal);

            // Search for user by name
            var users = userService.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));


            if (user == null)
            {
                throw new Exception($"User {username} not found");
            }

            bodyMetricRepository.UpdateUserBodyMetrics(user.Id, userWeight, userHeight, userTargetGoal);
        }
    }
}
