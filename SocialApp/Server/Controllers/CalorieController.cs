namespace Server.Controllers
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;
    using Server.Interfaces;

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
                return this.NotFound();  // If no calorie data found for the user
            }
            return calorie.DailyIntake;  // Return the daily calorie goal
        }

        [HttpGet("food/{userId}")]
        public ActionResult<double> GetFood(long userId)
        {
            var calorie = this.calorieRepository.GetCaloriesByUserId(userId);
            if (calorie == null)
            {
                return this.NotFound();
            }
            return calorie.CaloriesConsumed;  // Return the calories consumed
        }

        [HttpGet("exercise/{userId}")]
        public ActionResult<double> GetExercise(long userId)
        {
            var calorie = this.calorieRepository.GetCaloriesByUserId(userId);
            if (calorie == null)
            {
                return this.NotFound();
            }
            return calorie.CaloriesBurned;  // Return the calories burned
        }

        [HttpGet("{userId}")]
        public ActionResult<Calorie> GetCalorie(long userId)
        {
            var calorie = this.calorieRepository.GetCaloriesByUserId(userId);
            if (calorie == null)
            {
                return this.NotFound();
            }
            return calorie;  // Return the calories burned
        }
    }
}