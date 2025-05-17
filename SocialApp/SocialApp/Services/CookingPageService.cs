namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;
    using SocialApp.Repository;
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
        public void AddCookingSkill(string username, string cookingDescription)
        {

            int userId = CookingPageRepo.GetUserIdByName(username);
            int skillId = CookingPageRepo.GetCookingSkillIdByDescription(cookingDescription);

            CookingPageRepo.UpdateUserCookingSkill(userId, skillId);
        }
    }
}
