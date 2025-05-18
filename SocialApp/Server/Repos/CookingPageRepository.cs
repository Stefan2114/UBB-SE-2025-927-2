namespace Server.Repos
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using AppCommonClasses.Models;
    using AppCommonClasses.Enums;
    using Server.Data;
    using AppCommonClasses.Interfaces;

    public class CookingPageRepository : ICookingPageRepository
    {
        private readonly SocialAppDbContext dbContext;

        public CookingPageRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }

        public CookingPage GetByUserId(long userId)
        {
            // Get the user
            var user = this.dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null || !user.CookingSkillId.HasValue)
                return null;

            // Get the cooking skill separately
            var cookingSkill = this.dbContext.CookingSkills.FirstOrDefault(cs => cs.Id == user.CookingSkillId.Value);

            // Map to CookingPage object
            return new CookingPage
            {
                UserId = user.Id,
                CookingSkillId = user.CookingSkillId.Value,
                CookingSkill = cookingSkill
            };
        }

        public void UpdateUserCookingSkill(long userId, int cookingSkillId)
        {
            // Find the user directly, similar to BodyMetricRepository
            var user = this.dbContext.Users.Find(userId);
            if (user != null)
            {
                // Update the cooking skill ID
                user.CookingSkillId = cookingSkillId;
                this.dbContext.SaveChanges();
            }
        }

        public CookingSkill GetCookingSkillByDescription(string description)
        {
            return this.dbContext.CookingSkills
                .FirstOrDefault(cs => cs.Description == description);
        }
    }
}