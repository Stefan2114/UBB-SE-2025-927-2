using Microsoft.AspNetCore.Mvc;
using AppCommonClasses.Models;
using System.Linq;

namespace Server.Interfaces
{
    public class CookingSkillDTO
    {
        public int CookingSkillId { get; set; }
    }

    public interface ICookingPageController
    {
        IActionResult UpdateUserCookingSkill(int userId, [FromBody] CookingSkillDTO cookingSkill);// am schimbat din grs in DB cu int in loc de BIGINT si deai aam pus aici int in loc de long
        ActionResult<IQueryable<CookingSkill>> GetAllCookingSkills();
    }
}
