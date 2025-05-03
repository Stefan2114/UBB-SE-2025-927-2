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
        public int UserId { get; set; }

        [Column("i_name")]
        public string Name { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("calories")]
        public float Calories { get; set; }

        [Column("protein")]
        public float Protein { get; set; }

        [Column("carbohydrates")]
        public float Carbs { get; set; }

        [Column("fat")]
        public float Fats { get; set; }

        [Column("fiber")]
        public float Fiber { get; set; }

        [Column("sugar")]
        public float Sugar { get; set; }

        public Ingredient(int id, string name, string category, float calories, float protein, float carbs, float fats, float fiber, float sugar)
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