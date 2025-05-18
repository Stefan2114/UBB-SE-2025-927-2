using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface ICookingPageRepository
    {
        CookingPage GetByUserId(long userId);
        void UpdateUserCookingSkill(long userId, int cookingSkillId);
        CookingSkill GetCookingSkillByDescription(string description);
    }
}
