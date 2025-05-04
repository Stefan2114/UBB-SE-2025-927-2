using System.Linq;
using System;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Repos
{
    public class WaterIntakeRepository : IWaterIntakeRepository
    {
        private readonly SocialAppDbContext _dbContext;

        public WaterIntakeRepository(SocialAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUserIfNotExists(int userId)
        {
            var existing = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            if (existing == null)
            {
                var user = _dbContext.Users.Find(userId);
                if (user == null)
                {
                    throw new Exception($"User with ID {userId} not found.");
                }

                var newWater = new Water
                {
                    U_Id = userId,
                    WaterIntake = 0f,
                   
                };

                _dbContext.WaterTrackers.Add(newWater);
                _dbContext.SaveChanges();
            }
        }

        public float GetWaterIntake(int userId)
        {
            var tracker = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            return tracker?.WaterIntake ?? 0f;
        }

        public void UpdateWaterIntake(int userId, float newIntake)
        {
            var tracker = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            if (tracker != null)
            {
                tracker.WaterIntake = newIntake;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Water entry for user {userId} not found.");
            }
        }

        public void RemoveWater300(int userId) => RemoveWater(userId, 0.3f);
        public void RemoveWater400(int userId) => RemoveWater(userId, 0.4f);
        public void RemoveWater500(int userId) => RemoveWater(userId, 0.5f);
        public void RemoveWater750(int userId) => RemoveWater(userId, 0.75f);

        private void RemoveWater(int userId, float amount)
        {
            var tracker = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            if (tracker != null)
            {
                tracker.WaterIntake = Math.Max(0f, tracker.WaterIntake - amount);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Water entry for user {userId} not found.");
            }
        }
    }
}
