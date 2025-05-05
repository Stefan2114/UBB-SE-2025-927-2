using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("cooking_skills")]
    public class CookingSkill
    {
        [Key]
        [Column("cs_id")]
        public int Id { get; set; }

        [Required]
        [Column("cs_description")]
        public string Description { get; set; } = string.Empty;
    }
} 