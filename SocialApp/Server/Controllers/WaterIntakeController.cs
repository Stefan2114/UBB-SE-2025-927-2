using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("waterintake")]
    public class WaterIntakeController : ControllerBase, IWaterIntakeController
    {
        private readonly IWaterIntakeRepository waterRepository;

        public WaterIntakeController(IWaterIntakeRepository waterRepository)
        {
            this.waterRepository = waterRepository;
        }

        [HttpPost("{userId}")]
        public IActionResult AddUserIfNotExists(int userId)
        {
            try
            {
                waterRepository.AddUserIfNotExists(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public ActionResult<float> GetWaterIntake(int userId)
        {
            try
            {
                return Ok(waterRepository.GetWaterIntake(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{userId}")]
        public IActionResult UpdateWaterIntake(int userId, [FromQuery] float newIntake)
        {
            try
            {
                waterRepository.UpdateWaterIntake(userId, newIntake);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/300/{userId}")]
        public IActionResult RemoveWater300(int userId)
        {
            try
            {
                waterRepository.RemoveWater300(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/400/{userId}")]
        public IActionResult RemoveWater400(int userId)
        {
            try
            {
                waterRepository.RemoveWater400(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/500/{userId}")]
        public IActionResult RemoveWater500(int userId)
        {
            try
            {
                waterRepository.RemoveWater500(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove/750/{userId}")]
        public IActionResult RemoveWater750(int userId)
        {
            try
            {
                waterRepository.RemoveWater750(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
