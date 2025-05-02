using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using SocialApp.Interfaces;
using SocialApp.Proxies;

namespace SocialApp.Services
{
    public class CookingPageService : ICookingPageService
    {
        private readonly ICookingPageRepository cookingPageRepository;

        public CookingPageService()
        {
            cookingPageRepository = new CookingPageRepositoryProxy();
        }

        public void AddCookingSkill(string firstName, string lastName, string cookingDescription)
        {
            var cookingSkill = cookingPageRepository.GetCookingSkillByDescription(cookingDescription);
            if (cookingSkill != null)
            {
                cookingPageRepository.UpdateUserCookingSkill(1, cookingSkill.Id);
            }
        }
    }
}
