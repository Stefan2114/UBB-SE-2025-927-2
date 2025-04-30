namespace AppCommonClasses.Interfaces
{
    public interface IWaterIntakeRepository
    {
        void AddUserIfNotExists(int userId);

        float GetWaterIntake(int userId);

        void UpdateWaterIntake(int userId, float newIntake);
    }
}
