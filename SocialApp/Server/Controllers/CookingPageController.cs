using Microsoft.AspNetCore.Mvc;
using AppCommonClasses.Interfaces;
using Server.Interfaces;
using AppCommonClasses.Models;
using System.Linq;
using Server.Data;

namespace Server.Controllers
{
    [ApiController]
    [Route("cooking")]
    public class CookingPageController : ControllerBase
    {
        private readonly ICookingPageRepository cookingPageRepository;
        private readonly SocialAppDbContext dbContext;

        public CookingPageController(ICookingPageRepository cookingPageRepository, SocialAppDbContext dbContext)
        {
            this.cookingPageRepository = cookingPageRepository;
            this.dbContext = dbContext;
        }

        [HttpGet("skills")]
        public ActionResult<IQueryable<CookingSkill>> GetAllCookingSkills()
        {
            return Ok(dbContext.CookingSkills);
        }

        [HttpPut("user/{userId}")]
        public IActionResult UpdateUserCookingSkill(int userId, [FromBody] CookingSkillDTO cookingSkill) // am schimbat din grs in DB cu int in loc de BIGINT si deai aam pus aici int in loc de long
        {
            try
            {
                this.cookingPageRepository.UpdateUserCookingSkill((int)userId, cookingSkill.CookingSkillId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 