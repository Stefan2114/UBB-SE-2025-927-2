namespace AppCommonClasses.Interfaces
{
    using AppCommonClasses.Models;
    using System.Collections.Generic;

    public interface ICookingPageRepository
    {
        int GetUserIdByName(string firstName, string lastName);

        int GetCookingSkillIdByDescription(string description);

        void UpdateUserCookingSkill(int userId, int cookingSkillId);

        List<CookingSkill> GetAllCookingSkills();
    }
}
