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
        public float DailyIntake { get; set; }
        [Column("calories_consumed")]
        public float CaloriesConsumed { get; set; }
        [Column("calories_burned")]
        public float CaloriesBurned { get; set; }

        public virtual User? User { get; set; }

    }
}
