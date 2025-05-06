namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using global::Windows.Networking;
    using SocialApp.Interfaces;
    using SocialApp.Services;

    public class CalorieService : ICalorieService
    {
        private readonly ICalorieRepository calorieRepo;
        private readonly IUserService userService;

        public CalorieService(ICalorieRepository calorieRepository, IUserService userService)
        {
            this.calorieRepo = calorieRepository;
            this.userService = userService;
        }

        public double GetGoal(long userId)
        {
            var calories = this.calorieRepo.GetCaloriesByUserId(userId);
            return calories?.DailyIntake ?? 0; // Assuming DailyIntake is the "Goal"
        }

        public double GetFood(long userId)
        {
            var calories = this.calorieRepo.GetCaloriesByUserId(userId);
            return calories?.CaloriesConsumed ?? 0; // Assuming CaloriesConsumed is the "Food"
        }

        public double GetExercise(long userId)
        {
            var calories = this.calorieRepo.GetCaloriesByUserId(userId);
            return calories?.CaloriesBurned ?? 0; // Assuming CaloriesBurned is the "Exercise"
        }

        // Hardcoded setter methods
        public void SetHardcodedValues(long userId)
        {
            var users = userService.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Username.Equals($"{firstName} {lastName}", StringComparison.OrdinalIgnoreCase) ||
                                                u.Username.Equals($"{lastName} {firstName}", StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                throw new Exception($"User {firstName} {lastName} not found");
            }

            double hardcodedGoal = 2500;
            this.calorieRepo.SetDailyIntake(userId, hardcodedGoal);
            double hardcodedFood = 1800;
            this.calorieRepo.SetCaloriesConsumed(userId, hardcodedFood);
            double hardcodedExercise = 500;
            this.calorieRepo.SetCaloriesBurned(userId, hardcodedExercise);
        }
    }
}
