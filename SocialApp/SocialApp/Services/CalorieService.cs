namespace SocialApp.Services
{

    using System;
    using System.Collections.Generic;
    using AppCommonClasses.Models;
    using Server.Repos;
    using AppCommonClasses.Enums;
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;

    public class CalorieService : ICalorieService
    {
        private readonly ICalorieRepository _calorieRepository;

        public CalorieService(ICalorieRepository calorieRepository)
        {
            _calorieRepository = calorieRepository;
        }

        public double GetGoal(long userId)
        {
            var calories = this._calorieRepository.GetCaloriesByUserId(userId);
            return calories?.DailyIntake ?? 0; // Assuming DailyIntake is the "Goal"
        }

        public double GetFood(long userId)
        {
            var calories = this._calorieRepository.GetCaloriesByUserId(userId);
            return calories?.CaloriesConsumed ?? 0; // Assuming CaloriesConsumed is the "Food"
        }

        public double GetExercise(long userId)
        {
            var calories = this._calorieRepository.GetCaloriesByUserId(userId);
            return calories?.CaloriesBurned ?? 0; // Assuming CaloriesBurned is the "Exercise"
        }
    }
}
