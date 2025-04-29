namespace AppCommonClasses.Interfaces
{
    public interface ICookingPageRepository
    {
        int GetUserIdByName(string firstName, string lastName);

        int GetCookingSkillIdByDescription(string description);

        void UpdateUserCookingSkill(int userId, int cookingSkillId);
    }
}
