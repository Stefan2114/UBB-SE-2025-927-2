using System;
using System.Linq;
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

        public void AddUserIfNotExists(long userId)
        {
            var existing = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            if (existing == null)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    throw new Exception($"User with ID {userId} not found.");
                }

                var newWater = new Water
                {
                    U_Id = userId,
                    water_intake = 0f,
                };

                _dbContext.WaterTrackers.Add(newWater);
                _dbContext.SaveChanges();
            }
        }

        public double GetWaterIntake(long userId)
        {
            var tracker = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            return tracker?.water_intake ?? 0f;
        }

        public void UpdateWaterIntake(long userId, double newIntake)
        {
            var tracker = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            if (tracker != null)
            {
                tracker.water_intake = newIntake;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Water entry for user {userId} not found.");
            }
        }

        public void RemoveWater300(long userId) => RemoveWater(userId, 0.3f);
        public void RemoveWater400(long userId) => RemoveWater(userId, 0.4f);
        public void RemoveWater500(long userId) => RemoveWater(userId, 0.5f);
        public void RemoveWater750(long userId) => RemoveWater(userId, 0.75f);

        private void RemoveWater(long userId, double amount)
        {
            var tracker = _dbContext.WaterTrackers.FirstOrDefault(w => w.U_Id == userId);
            if (tracker != null)
            {
                tracker.water_intake = Math.Max(0f, tracker.water_intake - amount);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Water entry for user {userId} not found.");
            }
        }
    }
}
