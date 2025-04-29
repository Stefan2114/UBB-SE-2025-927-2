namespace AppCommonClasses.Models
{
    using System;

    public class Meal
    {
        public string Name { get; set; }

        public string Ingredients { get; set; }

        public int Calories { get; set; }

        public string Category { get; set; }

        public int Protein { get; set; }

        public int Carbohydrates { get; set; }

        public int Fat { get; set; }

        public int Fiber { get; set; }

        public int Sugar { get; set; }

        public string PhotoLink { get; set; }

        public string Recipe { get; set; }

        public int PreparationTime { get; set; }

        public int Servings { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CookingLevel { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }

        public Meal(string name, string ingredients, int calories, string category, string photoLink, string recipe)
        {
            Name = name;
            Ingredients = ingredients;
            Calories = calories;
            Category = category;
            PhotoLink = photoLink;
            Recipe = recipe;
            CreatedAt = DateTime.Now;
        }

        public Meal()
        {
            CreatedAt = DateTime.Now;
        }
    }

    public enum MealModel
    {
        SuccessfulCreationIndicator = 0,
        FailedOperationCode = -1,
        BreakfastTypeId = 1,
        LunchTypeId = 2,
        DinnerTypeId = 3,
        SnackTypeId = 4,
        DessertTypeId = 5,
        PostWorkoutTypeId = 6,
        PreWorkoutTypeId = 7,
        VeganMealTypeId = 8,
        HighProteinMealTypeId = 9,
        LowCarbMealTypeId = 10,
        DefaultMealTypeId = 1,
        BeginnerSkillId = 1,
        IntermediateSkillId = 2,
        AdvancedSkillId = 3,
        DefaultCookingSkillId = 1,
    }
}