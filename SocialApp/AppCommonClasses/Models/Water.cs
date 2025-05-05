namespace AppCommonClasses.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("water_trackers")]
    public class Water
    {
        [Key]
        [ForeignKey("User")]
        public long U_Id { get; set; } // Foreign key from Users table

        public double water_intake { get; set; } // Amount of water consumed

         public virtual User User { get; set; } // Navigation property
    }
}
