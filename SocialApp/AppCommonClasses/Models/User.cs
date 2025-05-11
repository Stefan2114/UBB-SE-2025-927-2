using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }  // Auto-incremented in DB

        [Column("UserName")]
        public string Username { get; set; }

        //[Column("u_date_of_birth")]
        //public DateTime? DateOfBirth { get; set; }

        [Column("Email")]
        public string Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Image { get; set; }

        [Column("u_height")]
        public double Height { get; set; }

        [Column("u_weight")]
        public double Weight { get; set; }

        [Column("target_weight")]
        public double? TargetWeight { get; set; }

        // Make these nullable to match your database
        [Column("g_id")]
        public int? GoalId { get; set; }

        [Column("cs_id")]
        public int? CookingSkillId { get; set; }

        [Column("dp_id")]
        public int? DietaryPreferenceId { get; set; }

        [Column("a_id")]
        public int? AllergyId { get; set; }

        [Column("al_id")]
        public int? ActivityLevelId { get; set; }
    }
}
