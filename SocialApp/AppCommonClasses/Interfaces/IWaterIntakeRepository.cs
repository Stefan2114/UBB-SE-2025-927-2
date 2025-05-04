namespace AppCommonClasses.Interfaces
{
    public interface IWaterIntakeRepository
    {
        void AddUserIfNotExists(int userId);

        float GetWaterIntake(int userId);

        void UpdateWaterIntake(int userId, float newIntake);

        void RemoveWater300(int userId); // Removes 0.3L
        void RemoveWater400(int userId); // Removes 0.4L
        void RemoveWater500(int userId); // Removes 0.5L
        void RemoveWater750(int userId); // Removes 0.75L
    }
}
