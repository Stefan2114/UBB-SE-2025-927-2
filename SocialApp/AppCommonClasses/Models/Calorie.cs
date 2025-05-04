namespace AppCommonClasses.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("calorie_trackers")]
    public class Calorie
    {
        [Key]
        [ForeignKey("User")]
        public long U_Id { get; set; }
        public DateTime Today { get; set; }

        [Column("daily_intake")]
        public double DailyIntake { get; set; }  // Change to double
        [Column("calories_consumed")]
        public double CaloriesConsumed { get; set; }  // Change to double
        [Column("calories_burned")]
        public double CaloriesBurned { get; set; }  // Change to double

        public virtual User? User { get; set; }
    }
}
