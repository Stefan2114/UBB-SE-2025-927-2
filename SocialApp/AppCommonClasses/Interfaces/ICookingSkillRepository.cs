using System;

namespace AppCommonClasses.Interfaces
{
    public interface ICookingSkillRepository
    {
        void UpdateUserCookingSkill(long userId, int cookingSkillId);
    }
} 