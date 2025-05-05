namespace AppCommonClasses.Interfaces
{
    public interface IWaterIntakeRepository
    {
        void AddUserIfNotExists(long userId);
        double GetWaterIntake(long userId);
        void UpdateWaterIntake(long userId, double newIntake);
        void RemoveWater300(long userId);
        void RemoveWater400(long userId);
        void RemoveWater500(long userId);
        void RemoveWater750(long userId);
    }

}
