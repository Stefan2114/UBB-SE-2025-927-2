using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface IWaterIntakeController
    {
        ActionResult<float> GetWaterIntake(int userId);
        IActionResult AddUserIfNotExists(int userId);
        IActionResult UpdateWaterIntake(int userId, float newIntake);
        IActionResult RemoveWater300(int userId);
        IActionResult RemoveWater400(int userId);
        IActionResult RemoveWater500(int userId);
        IActionResult RemoveWater750(int userId);
    }
}
