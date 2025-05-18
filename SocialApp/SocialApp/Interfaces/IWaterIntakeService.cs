namespace SocialApp.Interfaces
{
    public interface IWaterIntakeService
    {
        void AddUserIfNotExists(long userId);

        float GetWaterIntake(long userId);

        void RemoveWater300(long userId);

        void RemoveWater400(long userId);

        void RemoveWater500(long userId);

        void RemoveWater750(long userId);

        void UpdateWaterIntake(long userId, float newIntake);
    }
}