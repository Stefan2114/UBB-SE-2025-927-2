namespace Server.Controllers
{
    using AppCommonClasses.DTOs;
    using AppCommonClasses.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Server.Interfaces;

    [ApiController]
    [Route("bodymetrics")]
    public class BodyMetricController : ControllerBase, IBodyMetricController
    {
        private readonly IBodyMetricRepository bodyMetricRepository;

        public BodyMetricController(IBodyMetricRepository bodyMetricRepository)
        {
            this.bodyMetricRepository = bodyMetricRepository;
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
