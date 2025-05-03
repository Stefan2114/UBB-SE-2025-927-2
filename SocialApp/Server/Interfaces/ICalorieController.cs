using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface ICalorieController
    {
        ActionResult<double> GetGoal(long userId);
        ActionResult<double> GetFood(long userId);
        ActionResult<double> GetExercise(long userId);
    }
}
