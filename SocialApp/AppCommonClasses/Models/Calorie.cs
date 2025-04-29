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
        public int U_Id { get; set; }

        public DateTime Today { get; set; }

        public float DailyIntake { get; set; }

        public float CaloriesConsumed { get; set; }

        public float CaloriesBurned { get; set; }

        public virtual User? User { get; set; }

    }
}
