using System.ComponentModel.DataAnnotations;

namespace AppCommonClasses.Models
{
    public class CookingSkill
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
} 