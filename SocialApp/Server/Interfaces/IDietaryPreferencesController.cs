namespace Server.Interfaces
{
    using Microsoft.AspNetCore.Mvc;

    public interface IDietaryPreferencesController
    {
        IActionResult AddUserDietaryPreferenceIfNotExists(long userId, string dietaryPreference);
        ActionResult<string> GetUserDietaryPreference(long userId);
        IActionResult UpdateUserDietaryPreference(long userId, string newDietaryPreference);
        IActionResult RemoveUserDietaryPreference(long userId);
    }
} 