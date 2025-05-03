namespace Server.Interfaces
{
    using Microsoft.AspNetCore.Mvc;

    public interface ICalorieController
    {
        ActionResult<double> GetGoal(long userId);

        ActionResult<double> GetFood(long userId);

        ActionResult<double> GetExercise(long userId);
    }
}
