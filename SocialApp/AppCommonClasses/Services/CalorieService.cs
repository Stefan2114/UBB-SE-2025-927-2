using AppCommonClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.Services
{
    public class CalorieService : ICalorieService
    {
        private readonly ICalorieRepository calorieRepo;

        public CalorieService(ICalorieRepository calorieRepository)
        {
            this.calorieRepo = calorieRepository;
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
    }
}
