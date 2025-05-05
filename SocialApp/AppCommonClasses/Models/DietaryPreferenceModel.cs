namespace AppCommonClasses.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dietary_preferences")]
    public class DietaryPreferenceModel
    {
        [Key]
        [ForeignKey("User")]
        public long U_Id { get; set; } // Foreign key from Users table

        public string Description { get; set; } // Description of the dietary preference

        public string DietaryPreferences { get; set; } // Dietary preferences

        public string Allergies { get; set; } // Allergies

        public long Id { get; set; } // Primary key

        public virtual User User { get; set; } // Navigation property
    }

    public enum DietaryPreferenceType
    {
        NotSelected = -1,
    }
}
