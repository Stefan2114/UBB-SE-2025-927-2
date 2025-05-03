using AppCommonClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("calories")]
    public class CalorieController : ControllerBase, ICalorieController
    {
        private readonly ICalorieRepository calorieRepository;

        public CalorieController(ICalorieRepository calorieRepository)
        {
            this.calorieRepository = calorieRepository;
        }

        // Assuming the repository returns a Calorie object for the user
        [HttpGet("goal/{userId}")]
        public ActionResult<double> GetGoal(long userId)
        {
            var calorie = this.calorieRepository.GetCaloriesByUserId(userId);
            if (calorie == null)
            {
                return NotFound();  // If no calorie data found for the user
            }
            return calorie.DailyIntake;  // Return the daily calorie goal
        }

        [HttpGet("food/{userId}")]
        public ActionResult<double> GetFood(long userId)
        {
            var calorie = this.calorieRepository.GetCaloriesByUserId(userId);
            if (calorie == null)
            {
                return NotFound();
            }
            return calorie.CaloriesConsumed;  // Return the calories consumed
        }

        [HttpGet("exercise/{userId}")]
        public ActionResult<double> GetExercise(long userId)
        {
            var calorie = this.calorieRepository.GetCaloriesByUserId(userId);
            if (calorie == null)
            {
                return NotFound();
            }
            return calorie.CaloriesBurned;  // Return the calories burned
        }
    }
}