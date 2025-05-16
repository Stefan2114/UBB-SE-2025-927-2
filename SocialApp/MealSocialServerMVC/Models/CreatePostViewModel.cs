using System.ComponentModel.DataAnnotations;
using AppCommonClasses.Enums;

namespace MealSocialServerMVC.Models
{
    public class CreatePostViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Content { get; set; }

        [Required]
        public PostVisibility Visibility { get; set; }

        [Required]
        public PostTag Tag { get; set; }
    }
}
