using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface ICookingPageController
    {
        ActionResult<CookingPage> GetByUserId(long userId);
        IActionResult UpdateUserCookingSkill(long userId, int cookingSkillId);
        ActionResult<CookingSkill> GetCookingSkillByDescription(string description);
    }
} 