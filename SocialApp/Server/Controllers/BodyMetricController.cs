using AppCommonClasses.DTOs;
using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("bodymetrics")]
    public class BodyMetricController : ControllerBase, IBodyMetricController
    {
        private readonly IBodyMetricRepository bodyMetricRepository;
        private readonly IUserService userService;

        public BodyMetricController(IBodyMetricRepository bodyMetricRepository, IUserService userService)
        {
            this.bodyMetricRepository = bodyMetricRepository;
            this.userService = userService;
        }

        [HttpPut("update")]
        public IActionResult UpdateUserBodyMetrics([FromBody] BodyMetricDTO bodyMetric)
        {
            try
            {
                // Find user by username
                var user = userService.GetUserByUsername(bodyMetric.Username);
                if (user == null)
                {
                    return NotFound($"User {bodyMetric.Username} not found");
                }

                this.bodyMetricRepository.UpdateUserBodyMetrics(
                    user.Id,
                    bodyMetric.Weight,
                    bodyMetric.Height,
                    bodyMetric.TargetWeight);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("user/{userId}")]
        public IActionResult UpdateUserBodyMetrics(long userId, [FromBody] BodyMetricDTO bodyMetric)
        {
            try
            {
                this.bodyMetricRepository.UpdateUserBodyMetrics(
                    userId,
                    bodyMetric.Weight,
                    bodyMetric.Height,
                    bodyMetric.TargetWeight);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
