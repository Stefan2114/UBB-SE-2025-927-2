using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface ICookingPageController
    {
        ActionResult<CookingPage> GetByUserId(int userId);
        IActionResult UpdateUserCookingSkill(int userId, int cookingSkillId);
        ActionResult<CookingSkill> GetCookingSkillByDescription(string description);
    }
} 