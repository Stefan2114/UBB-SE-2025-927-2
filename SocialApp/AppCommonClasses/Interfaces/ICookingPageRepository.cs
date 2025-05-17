namespace AppCommonClasses.Interfaces
{
    public interface ICookingPageRepository
    {
        int GetUserIdByName(string username);

        int GetCookingSkillIdByDescription(string description);

        void UpdateUserCookingSkill(int userId, int cookingSkillId);
    }
}
