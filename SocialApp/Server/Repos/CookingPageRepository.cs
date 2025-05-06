using System;
using System.Linq;
using System.Collections.Generic;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
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

        public int GetUserIdByName(string firstName, string lastName)
        {
            var fullName = $"{lastName} {firstName}";
            var user = dbContext.Users.FirstOrDefault(u => u.Name == fullName);
            return user != null ? user.Id : 0;
        }

        public int GetCookingSkillIdByDescription(string description)
        {
            var cookingSkill = dbContext.CookingSkills.FirstOrDefault(cs => cs.Description == description);
            return cookingSkill?.Id ?? 0;
        }

        public void UpdateUserCookingSkill(int userId, int cookingSkillId)
        {
            var user = dbContext.Users.Find(userId);
            if (user != null)
            {
                // Check if the cooking skill exists
                var cookingSkill = dbContext.CookingSkills.Find(cookingSkillId);
                if (cookingSkill == null)
                {
                    throw new ArgumentException($"Cooking skill with ID {cookingSkillId} does not exist");
                }

                // Update or assign the cooking skill
                user.CookingSkillId = cookingSkillId;
                dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"User with ID {userId} does not exist");
            }
        }

        public List<CookingSkill> GetAllCookingSkills()
        {
            return dbContext.CookingSkills.ToList();
        }
    }
}
