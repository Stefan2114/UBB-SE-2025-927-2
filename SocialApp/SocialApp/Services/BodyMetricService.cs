namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using System;
    using System.Linq;
    using System.Diagnostics;

    public class BodyMetricService : IBodyMetricService
    {
        private readonly IBodyMetricRepository bodyMetricRepository;
        private readonly IUserService userService;

        public BodyMetricService(IBodyMetricRepository bodyMetricRepository, IUserService userService)
        {
            this.bodyMetricRepository = bodyMetricRepository;
            this.userService = userService;
        }

        // Implementation that matches the interface signature
        public void UpdateUserBodyMetrics(string username, float weight, float height, float? targetWeight)
        {
            try
            {
                Debug.WriteLine($"Updating metrics for user: {username}");

                // Search for user by name
                var users = userService.GetAllUsers();
                var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    Debug.WriteLine($"User not found: {username}");
                    throw new Exception($"User {username} not found");
                }

                Debug.WriteLine($"Found user with ID: {user.Id}");
                bodyMetricRepository.UpdateUserBodyMetrics(user.Id, weight, height, targetWeight);
                Debug.WriteLine("Body metrics updated successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateUserBodyMetrics: {ex}");
                throw;
            }
        }

        // Keep the original method as an overload that accepts strings
        public void UpdateUserBodyMetrics(string username, string weightStr, string heightStr, string targetWeightStr)
        {
            float weight = float.Parse(weightStr);
            float height = float.Parse(heightStr);
            float? targetWeight = string.IsNullOrWhiteSpace(targetWeightStr) ? null : float.Parse(targetWeightStr);

            // Call the interface-compliant method
            UpdateUserBodyMetrics(username, weight, height, targetWeight);
        }
    }
}
