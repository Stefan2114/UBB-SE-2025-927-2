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
        private readonly IBodyMetricService _bodyMetricService;
        private readonly IUserService _userService;

        public BodyMetricController(IBodyMetricService bodyMetricService, IUserService userService)
        {
            this._bodyMetricService = bodyMetricService;
            this._userService = userService;
        }

        [HttpPut("update")]
        public IActionResult UpdateUserBodyMetrics([FromBody] BodyMetricDTO bodyMetric)
        {
            try
            {
                // Use the service instead of directly working with the repository
                _bodyMetricService.UpdateUserBodyMetrics(
                    bodyMetric.Username,
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
                // Get the username for the user ID
                var user = _userService.GetById(userId);
                if (user == null)
                {
                    return NotFound($"User with ID {userId} not found");
                }

                // Use the service instead of the repository
                _bodyMetricService.UpdateUserBodyMetrics(
                    user.Username,
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
