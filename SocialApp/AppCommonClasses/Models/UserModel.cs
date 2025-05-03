namespace AppCommonClasses.Models
{
    public class UserModel
    {
        public int UserId { get; set; }  // Auto-incremented in DB
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double? TargetWeight { get; set; }
        public int GoalId { get; set; }
        public int CookingSkillId { get; set; }
        public int DietaryPreferenceId { get; set; }
        public int AllergyId { get; set; }
        public int ActivityLevelId { get; set; }
    }
}
