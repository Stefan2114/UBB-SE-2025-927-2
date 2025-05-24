using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Column("iamge")]
        public string? Image { get; set; }

        [Required]
        [Column("gender")]
        public string Gender { get; set; }

        [Required]
        [Column("height")]
        [Range(1, double.MaxValue, ErrorMessage = "Height must be a positive number.")]
        public double Height { get; set; }

        [Required]
        [Column("weight")]
        [Range(1, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public double Weight { get; set; }

        [Required]
        [Column("goal")]
        public string Goal { get; set; }

        [Required]
        [Column("activity_level")]
        public string ActivityLevel { get; set; }

    }
}
