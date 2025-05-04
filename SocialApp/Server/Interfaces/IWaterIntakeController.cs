namespace Server.Interfaces
{
    using Microsoft.AspNetCore.Mvc;

    public interface IWaterIntakeController
    {
        IActionResult AddUserIfNotExists(int userId);
        ActionResult<float> GetWaterIntake(int userId);
        IActionResult UpdateWaterIntake(int userId, float newIntake);
        IActionResult RemoveWater300(int userId);
        IActionResult RemoveWater400(int userId);
        IActionResult RemoveWater500(int userId);
        IActionResult RemoveWater750(int userId);
    }
}
