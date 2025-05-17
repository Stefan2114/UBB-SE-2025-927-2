using AppCommonClasses.DTOs;
using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Server.Controllers
{
    [ApiController]
    [Route("bodymetrics")]
    public class BodyMetricController : ControllerBase
    {
        private readonly IBodyMetricRepository bodyMetricRepository;
        private readonly IUserService userService;

        public BodyMetricController(IBodyMetricRepository bodyMetricRepository, IUserService userService)
        {
            this.bodyMetricRepository = bodyMetricRepository;
            this.userService = userService;
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
