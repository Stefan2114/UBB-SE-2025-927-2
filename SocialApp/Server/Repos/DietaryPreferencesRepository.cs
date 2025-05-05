using System;
using System.Linq;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Repos
{
    public class DietaryPreferencesRepository : IDietaryPreferencesRepository
    {
        private readonly SocialAppDbContext _dbContext;

        public DietaryPreferencesRepository(SocialAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUserDietaryPreferenceIfNotExists(long userId, string dietaryPreference)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            if (user.DietaryPreferenceId == null)
            {
                var preference = _dbContext.DietaryPreferences.FirstOrDefault(dp => dp.Description == dietaryPreference);
                if (preference == null)
                {
                    throw new Exception($"Dietary preference '{dietaryPreference}' not found.");
                }

                user.DietaryPreferenceId = (int?)preference.Id; // Explicit cast to int?
                _dbContext.SaveChanges();
            }
        }

        public string GetUserDietaryPreference(long userId)
        {
            
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            
            var preference = _dbContext.DietaryPreferences.FirstOrDefault(dp => dp.Id == user.DietaryPreferenceId);
            return preference?.Description ?? "No dietary preference set.";
        }

        public void UpdateUserDietaryPreference(long userId, string newDietaryPreference)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            var preference = _dbContext.DietaryPreferences.FirstOrDefault(dp => dp.Description == newDietaryPreference);
            if (preference == null)
            {
                throw new Exception($"Dietary preference '{newDietaryPreference}' not found.");
            }

            user.DietaryPreferenceId = (int?)preference.Id; // Explicit cast to int?
            _dbContext.SaveChanges();
        }

        public void RemoveUserDietaryPreference(long userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            user.DietaryPreferenceId = null;
            _dbContext.SaveChanges();
        }
    }
}
