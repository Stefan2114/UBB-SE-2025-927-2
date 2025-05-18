using System.ComponentModel.DataAnnotations;

namespace MealSocialServerMVC.Models
{
    public class CaloriesViewModel
    {
        public string Username { get; set; }

        // Calorie tracking data
        public double DailyGoal { get; set; }
        public double CaloriesConsumed { get; set; }
        public double CaloriesBurned { get; set; }

        // Computed property for remaining calories
        public double RemainingCalories => DailyGoal - CaloriesConsumed + CaloriesBurned;

        // Optional tracking percentage for visual display
        public int CompletionPercentage =>
            (int)Math.Clamp((CaloriesConsumed / DailyGoal) * 100, 0, 100);
    }
}
