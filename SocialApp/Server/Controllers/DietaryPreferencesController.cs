using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("dietarypreferences")]
    public class DietaryPreferencesController : ControllerBase
    {
        private readonly IDietaryPreferencesRepository dietaryRepository;

        public DietaryPreferencesController(IDietaryPreferencesRepository dietaryRepository)
        {
            this.dietaryRepository = dietaryRepository;
        }

        [HttpPost("{userId}")]
        public IActionResult AddUserDietaryPreferenceIfNotExists(long userId, [FromQuery] string dietaryPreference)
        {
            try
            {
                dietaryRepository.AddUserDietaryPreferenceIfNotExists(userId, dietaryPreference);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public ActionResult<string> GetUserDietaryPreference(long userId)
        {
            try
            {
                return Ok(dietaryRepository.GetUserDietaryPreference(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{userId}")]
        public IActionResult UpdateUserDietaryPreference(long userId, [FromQuery] string newDietaryPreference)
        {
            try
            {
                dietaryRepository.UpdateUserDietaryPreference(userId, newDietaryPreference);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/{userId}")]
        public IActionResult RemoveUserDietaryPreference(long userId)
        {
            try
            {
                dietaryRepository.RemoveUserDietaryPreference(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 