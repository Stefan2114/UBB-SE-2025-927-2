using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    [Table("cooking_page")]
    public class CookingPage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("user_id")]
        public long UserId { get; set; }
        
        [Required]
        [Column("cooking_skill_id")]
        public int CookingSkillId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        
        [ForeignKey("CookingSkillId")]
        public CookingSkill CookingSkill { get; set; }
    }
} 