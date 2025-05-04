namespace Server.Controllers
{
    using AppCommonClasses.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Server.Interfaces;

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
                this.waterRepository.AddUserIfNotExists(userId);
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
                var intake = this.waterRepository.GetWaterIntake(userId);
                return Ok(intake);
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
                this.waterRepository.UpdateWaterIntake(userId, newIntake);
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
                this.waterRepository.RemoveWater300(userId);
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
                this.waterRepository.RemoveWater400(userId);
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
                this.waterRepository.RemoveWater500(userId);
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
                this.waterRepository.RemoveWater750(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
