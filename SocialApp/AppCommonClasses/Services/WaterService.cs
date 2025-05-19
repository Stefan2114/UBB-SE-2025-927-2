using System;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Repos;

namespace AppCommonClasses.Services
{
    public class WaterService : IWaterIntakeService
    {
        private readonly IWaterIntakeRepository waterIntakeRepository;

        public WaterService(IWaterIntakeRepository waterIntakeRepository)
        {
            this.waterIntakeRepository = waterIntakeRepository;
        }

        public void AddUserIfNotExists(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            waterIntakeRepository.AddUserIfNotExists(userId);
        }

        public float GetWaterIntake(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            return (float)waterIntakeRepository.GetWaterIntake(userId);
        }

        public void UpdateWaterIntake(long userId, float newIntake)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            if (newIntake < 0)
            {
                throw new ArgumentException("Water intake cannot be negative.", nameof(newIntake));
            }
            waterIntakeRepository.UpdateWaterIntake(userId, newIntake);
        }

        public void RemoveWater300(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            waterIntakeRepository.RemoveWater300(userId);
        }

        public void RemoveWater400(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            waterIntakeRepository.RemoveWater400(userId);
        }

        public void RemoveWater500(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            waterIntakeRepository.RemoveWater500(userId);
        }

        public void RemoveWater750(long userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            }
            waterIntakeRepository.RemoveWater750(userId);
        }
    }
}