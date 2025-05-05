namespace Server.Interfaces
{
    using AppCommonClasses.DTOs;
    using Microsoft.AspNetCore.Mvc;

    public interface IBodyMetricController
    {
        /// <summary>
        /// Updates body metrics for a specific user
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="bodyMetric">The body metric data</param>
        /// <returns>Action result indicating success or failure</returns>
        IActionResult UpdateUserBodyMetrics(long userId, [FromBody] BodyMetricDTO bodyMetric);
    }
}
