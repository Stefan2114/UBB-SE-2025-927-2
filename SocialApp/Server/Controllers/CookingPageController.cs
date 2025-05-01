using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("cooking")]
    public class CookingPageController : ControllerBase, ICookingPageController
    {
        private readonly ICookingPageRepository cookingPageRepository;

        public CookingPageController(ICookingPageRepository cookingPageRepository)
        {
            this.cookingPageRepository = cookingPageRepository;
        }

        [HttpGet("user/{userId}")]
        public ActionResult<CookingPage> GetByUserId(int userId)
        {
            return this.cookingPageRepository.GetByUserId(userId);
        }

        [HttpPut("user/{userId}/skill/{cookingSkillId}")]
        public IActionResult UpdateUserCookingSkill(int userId, int cookingSkillId)
        {
            this.cookingPageRepository.UpdateUserCookingSkill(userId, cookingSkillId);
            return Ok();
        }

        [HttpGet("skill/{description}")]
        public ActionResult<CookingSkill> GetCookingSkillByDescription(string description)
        {
            return this.cookingPageRepository.GetCookingSkillByDescription(description);
        }
    }
}
