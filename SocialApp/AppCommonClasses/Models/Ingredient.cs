namespace AppCommonClasses.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ingredients")]
    public class Ingredient
    {
        [Key]
        [Column("i_id")]
        public int Id { get; set; }

        [Column("u_id")]
        public long UserId { get; set; }

        [Column("i_name")]
        public string Name { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("calories")]
        public double Calories { get; set; }

        [Column("protein")]
        public double Protein { get; set; }

        [Column("carbohydrates")]
        public double Carbs { get; set; }

        [Column("fat")]
        public double Fats { get; set; }

        [Column("fiber")]
        public double Fiber { get; set; }

        [Column("sugar")]
        public double Sugar { get; set; }

        public Ingredient(int id, string name, string category, double calories, double protein, double carbs, double fats, double fiber, double sugar)
        {
            Id = id;
            Name = name;
            Category = category;
            Calories = calories;
            Protein = protein;
            Carbs = carbs;
            Fats = fats;
            Fiber = fiber;
            Sugar = sugar;
        }

        public Ingredient()
        {
        }

        public static Ingredient NoIngredient { get; private set; } = new Ingredient(-1, string.Empty, string.Empty, -1, -1, -1, -1, -1, -1);
    }
}