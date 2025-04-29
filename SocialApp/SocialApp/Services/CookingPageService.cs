namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;
    using System;
    using System.Diagnostics;

    public class CookingPageService : ICookingPageService
    {
        private readonly ICookingPageRepository CookingPageRepo;

        public CookingPageService()
        {
            CookingPageRepo = new CookingPageRepository();
        }

        [Obsolete]
        public void AddCookingSkill(string firstName, string lastName, string cookingDescription)
        {
            Debug.WriteLine($"Adding cooking skill {cookingDescription} for user {firstName} {lastName}");

            int userId = CookingPageRepo.GetUserIdByName(firstName, lastName);
            int skillId = CookingPageRepo.GetCookingSkillIdByDescription(cookingDescription);

            CookingPageRepo.UpdateUserCookingSkill(userId, skillId);
        }
    }
}
