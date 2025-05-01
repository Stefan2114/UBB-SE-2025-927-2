using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCommonClasses.Models
{
    public class CookingPage
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int CookingSkillId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        
        [ForeignKey("CookingSkillId")]
        public CookingSkill CookingSkill { get; set; }
    }
} 