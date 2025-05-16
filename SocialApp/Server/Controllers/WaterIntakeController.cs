using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("waterintake")]
    public class WaterIntakeController : ControllerBase, IWaterIntakeController
    {
        private readonly IWaterIntakeService waterService;

        public WaterIntakeController(IWaterIntakeService waterService)
        {
            this.waterService = waterService;
        }

        [HttpPost("{userId}")]
        public IActionResult AddUserIfNotExists(int userId)
        {
            try
            {
                this.waterService.AddUserIfNotExists(userId);
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
                return Ok(waterService.GetWaterIntake(userId));
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
                waterService.UpdateWaterIntake(userId, newIntake);
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
                waterService.RemoveWater300(userId);
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
                waterService.RemoveWater400(userId);
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
                waterService.RemoveWater500(userId);
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
                waterService.RemoveWater750(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
