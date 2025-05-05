namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;
    using SocialApp.Proxies;
    using System;
    using System.Diagnostics;

    public class CookingPageService : ICookingPageService
    {
        private readonly ICookingPageRepository cookingPageRepo;

        public CookingPageService()
        {
            cookingPageRepo = new CookingPageRepositoryProxy();
        }

        public void AddCookingSkill(string firstName, string lastName, string cookingDescription)
        {
            Debug.WriteLine($"Adding cooking skill {cookingDescription} for user {firstName} {lastName}");

            int userId = cookingPageRepo.GetUserIdByName(firstName, lastName);
            int skillId = cookingPageRepo.GetCookingSkillIdByDescription(cookingDescription);

            cookingPageRepo.UpdateUserCookingSkill(userId, skillId);
        }
    }
}
