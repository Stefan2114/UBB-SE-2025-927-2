namespace SocialApp.Interfaces
{
    public interface ICalorieService
    {
        float GetGoal(int userId);

        float GetFood(int userId);

        float GetExercise(int userId);
    }
}
