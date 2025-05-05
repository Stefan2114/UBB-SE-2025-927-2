using System.ComponentModel.DataAnnotations;


namespace AppCommonClasses.Models
{
    public class MealIngredient
    {
        [Key]
        public int IngredientId { get; set; }
        public int MealId { get; set; }

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
                IngredientId = IngredientId,
                IngredientName = IngredientName,
                Quantity = Quantity,
                Protein = Protein * Quantity,
                Calories = Calories * Quantity,
                Carbs = Carbs * Quantity,
                Fats = Fats * Quantity,
                Fiber = Fiber * Quantity,
                Sugar = Sugar * Quantity
            };
        }
    }
}