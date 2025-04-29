using System;

namespace MealPlannerProject.Models
{
    public class MealIngredient
    {
        public int IngredientId { get; set; }

        public float Quantity { get; set; }

        public string IngredientName { get; set; }

        public float Protein { get; set; }

        public float Calories { get; set; }

        public float Carbs { get; set; }

        public float Fats { get; set; }

        public float Fiber { get; set; }

        public float Sugar { get; set; }

        // Calculate macros based on quantity
        public MealIngredient CalculateMacros()
        {
            return new MealIngredient
            {
                IngredientId = this.IngredientId,
                IngredientName = this.IngredientName,
                Quantity = this.Quantity,
                Protein = this.Protein * this.Quantity,
                Calories = this.Calories * this.Quantity,
                Carbs = this.Carbs * this.Quantity,
                Fats = this.Fats * this.Quantity,
                Fiber = this.Fiber * this.Quantity,
                Sugar = this.Sugar * this.Quantity
            };
        }
    }
}