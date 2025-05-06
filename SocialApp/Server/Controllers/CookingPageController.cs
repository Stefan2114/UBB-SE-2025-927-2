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
        public IActionResult UpdateUserCookingSkill(int userId, [FromBody] CookingSkillDTO cookingSkill)
        {
            try
            {
                // Validate the cooking skill ID
                if (cookingSkill.CookingSkillId <= 0)
                {
                    return BadRequest("Invalid cooking skill ID");
                }

                // Check if the cooking skill exists
                var skillExists = dbContext.CookingSkills.Any(cs => cs.Id == cookingSkill.CookingSkillId);
                if (!skillExists)
                {
                    return NotFound($"Cooking skill with ID {cookingSkill.CookingSkillId} not found");
                }

                // Check if the user exists
                var userExists = dbContext.Users.Any(u => u.Id == userId);
                if (!userExists)
                {
                    return NotFound($"User with ID {userId} not found");
                }

                this.cookingPageRepository.UpdateUserCookingSkill(userId, cookingSkill.CookingSkillId);
                return Ok(new { message = "Cooking skill updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
} 