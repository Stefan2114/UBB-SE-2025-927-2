using System.ComponentModel.DataAnnotations;

namespace MealSocialServerMVC.Models
{
    public class UpdateBodyMetricViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500 kg")]
        public float Weight { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Range(1, 300, ErrorMessage = "Height must be between 1 and 300 cm")]
        public float Height { get; set; }

        [Range(1, 500, ErrorMessage = "Target weight must be between 1 and 500 kg")]
        public float? TargetWeight { get; set; }
    }
}
