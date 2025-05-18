using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.DTOs
{
    public class MacrosDTO
    {
        public int DailyMealId { get; set; }
        public DateTime DateEaten { get; set; }
        public float Servings { get; set; }
        public string UnitName { get; set; }
        public float? TotalCalories { get; set; }
        public float? TotalProtein { get; set; }
        public float? TotalCarbohydrates { get; set; }
        public float? TotalFat { get; set; }
        public float? TotalFiber { get; set; }
        public float? TotalSugar { get; set; }

        public string? MealName { get; set; }  // Example if joined with Meal
        public string? UserName { get; set; }  // Example if joined with User
    }
}
