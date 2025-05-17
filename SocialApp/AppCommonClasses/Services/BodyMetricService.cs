using AppCommonClasses.Interfaces;
using System;
using System.Linq;
using System.Diagnostics;

namespace AppCommonClasses.Services
{
    public class BodyMetricService : IBodyMetricService
    {
        private readonly IBodyMetricRepository bodyMetricRepository;
        private readonly IUserService userService;

        public BodyMetricService(IBodyMetricRepository bodyMetricRepository, IUserService userService)
        {
            this.bodyMetricRepository = bodyMetricRepository ?? throw new ArgumentNullException(nameof(bodyMetricRepository));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public void UpdateUserBodyMetrics(string username, float weight, float height, float? targetWeight)
        {
            try
            {
                Debug.WriteLine($"Searching for user: {username}");

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
    }
}
