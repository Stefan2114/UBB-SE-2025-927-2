namespace SocialApp.Interfaces
{
    public interface ICalorieService
    {
        double GetGoal(long userId);

        double GetFood(long userId);

        double GetExercise(long userId);
        void SetHardcodedValues(long userId);
    }
}
