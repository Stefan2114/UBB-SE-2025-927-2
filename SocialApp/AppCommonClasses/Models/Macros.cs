namespace MealPlanner.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Windows.System;

    [Table("daily_meals")]
    public class Macros
    {
        [Key]
        [ForeignKey("User")]
        public int User_Id { get; set; } // Foreign key from Users table

        [Column ("total_protein")]
        public float TotalConsumedProtein { get; set; } // Total protein consumed

        [Column("total_carbohydrates")]
        public float TotalConsumedCarbohydrates { get; set; } // Total carbs consumed

        [Column("total_fat")]
        public float TotalConsumedFat { get; set; } // Total fat consumed

        [Column("total_fiber")]
        public float TotalConsumedFiber { get; set; } // Total fiber consumed

        [Column("total_sugar")]
        public float TotalConsumedSugar { get; set; } // Total sugar consumed

        public virtual User? UserNavigation { get; set; } // Navigation property
    }
}
