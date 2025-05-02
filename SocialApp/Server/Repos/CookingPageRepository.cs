using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Repos
{
    public class CookingPageRepository : ICookingPageRepository
    {
        private readonly SocialAppDbContext dbContext;

        public CookingPageRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }

        public CookingPage GetByUserId(int userId)
        {
            return this.dbContext.CookingPages
                .Include(cp => cp.CookingSkill)
                .FirstOrDefault(cp => cp.UserId == userId);
        }

        public void UpdateUserCookingSkill(int userId, int cookingSkillId)
        {
            var cookingPage = this.dbContext.CookingPages.FirstOrDefault(cp => cp.UserId == userId);
            
            if (cookingPage == null)
            {
                cookingPage = new CookingPage
                {
                    UserId = userId,
                    CookingSkillId = cookingSkillId
                };
                this.dbContext.CookingPages.Add(cookingPage);
            }
            else
            {
                cookingPage.CookingSkillId = cookingSkillId;
            }

            this.dbContext.SaveChanges();
        }

        public CookingSkill GetCookingSkillByDescription(string description)
        {
            return this.dbContext.CookingSkills
                .FirstOrDefault(cs => cs.Description == description);
        }
    }
} 