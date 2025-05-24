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
        [Range(1, 500, ErrorMessage = "Height must be a positive number and lower than 500.")]
        public int Height { get; set; }

        [Required]
        [Column("weight")]
        [Range(typeof(decimal), "1", "500" ,ErrorMessage = "Weight must be a positive number and lower than 500.")]
        public decimal Weight { get; set; }

        [Required]
        [Column("goal")]
        public string Goal { get; set; }

        [Required]
        [Column("activity_level")]
        public string ActivityLevel { get; set; }

        [Required]
        [Column("water_consumed")]
        [Range(typeof(decimal), "0", "100", ErrorMessage = "Water consumed must be a positive number and lower than 100.")]
        public decimal WaterConsumed { get; set; }

    }
}
