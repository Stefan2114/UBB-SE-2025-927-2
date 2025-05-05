using Microsoft.AspNetCore.Mvc;
using AppCommonClasses.Models;

namespace Server.Interfaces
{
    public interface ICookingSkillController
    {
        IActionResult GetCookingSkillByDescription(string description);
    }
} 