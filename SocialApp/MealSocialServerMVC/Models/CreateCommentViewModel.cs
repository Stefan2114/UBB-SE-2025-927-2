using System.ComponentModel.DataAnnotations;

namespace MealSocialServerMVC.Models
{
    public class CreateCommentViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }

        [Required]
        public long PostId { get; set; }

        // Optionally, include UserId if you want to set it from the UI
        // [Required]
        // public long UserId { get; set; }
    }
}
