using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        [Column("u_id")]
        public int Id { get; set; }  // am schimbat din grs in DB cu int in loc de BIGINT si deai aam pus aici int in loc de long

        [Column("u_name")]
        public string Name { get; set; }

        [Column("dob")]
        public DateTime? DateOfBirth { get; set; }

        [Column("email")]
        public string Email { get; set; }

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
