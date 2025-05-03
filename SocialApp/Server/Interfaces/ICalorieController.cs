using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface ICalorieController
    {
        ActionResult<float> GetGoal(int userId);
        ActionResult<float> GetFood(int userId);
        ActionResult<float> GetExercise(int userId);
    }
}
