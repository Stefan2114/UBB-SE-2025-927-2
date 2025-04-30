namespace AppCommonClasses.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("water_trackers")]
    public class Water
    {
        [Key]
        [ForeignKey("User")]
        public int U_Id { get; set; } // Foreign key from Users table

        public float WaterIntake { get; set; } // Amount of water consumed

        required public virtual User User { get; set; } // Navigation property
    }
}
