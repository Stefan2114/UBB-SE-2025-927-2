namespace AppCommonClasses.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("daily_meals")]
    public class Macros
    {
        [Key]
        [Column("dm_id")]
        public int DailyMealId { get; set; }

        [Column("u_id")]
        public long? UserId { get; set; }  // Nullable FK

        [Column("m_id")]
        public int MealId { get; set; }

        [Column("date_eaten")]
        public DateTime DateEaten { get; set; } = DateTime.Now;

        [Column("servings")]
        public double? Servings { get; set; }

        [Column("unit_name")]
        public string UnitName { get; set; }

        [Column("total_calories")]
        public double? TotalCalories { get; set; }

        [Column("total_protein")]
        public double? TotalProtein { get; set; }

        [Column("total_carbohydrates")]
        public double? TotalCarbohydrates { get; set; }

        [Column("total_fat")]
        public double? TotalFat { get; set; }

        [Column("total_fiber")]
        public double? TotalFiber { get; set; }

        [Column("total_sugar")]
        public double? TotalSugar { get; set; }

        // Navigation properties (optional but recommended if you use EF relationships)
        public virtual User? User { get; set; }

        public virtual Meal? Meal { get; set; }

        //public virtual ServingUnitModel? ServingUnit { get; set; }
    }
}
