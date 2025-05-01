using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface ICookingPageRepository
    {
        CookingPage GetByUserId(int userId);
        void UpdateUserCookingSkill(int userId, int cookingSkillId);
        CookingSkill GetCookingSkillByDescription(string description);
    }
}
