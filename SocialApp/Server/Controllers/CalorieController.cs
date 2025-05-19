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
        private readonly ICalorieService _calorieService;

        public CalorieController(ICalorieService calorieService)
        {
            this._calorieService = calorieService;
        }

        [HttpGet("goal/{userId}")]
        public ActionResult<double> GetGoal(long userId)
        {
            try
            {
                double goal = _calorieService.GetGoal(userId);
                return goal;
            }
            catch (Exception ex)
            {
                return NotFound($"Error retrieving calorie goal: {ex.Message}");
            }
        }

        [HttpGet("food/{userId}")]
        public ActionResult<double> GetFood(long userId)
        {
            try
            {
                double food = _calorieService.GetFood(userId);
                return food;
            }
            catch (Exception ex)
            {
                return NotFound($"Error retrieving food calories: {ex.Message}");
            }
        }

        [HttpGet("exercise/{userId}")]
        public ActionResult<double> GetExercise(long userId)
        {
            try
            {
                double exercise = _calorieService.GetExercise(userId);
                return exercise;
            }
            catch (Exception ex)
            {
                return NotFound($"Error retrieving exercise calories: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public ActionResult<Calorie> GetCalorie(long userId)
        {
            try
            {
                // For getting the full Calorie object, we still need to use the repository
                // Ideally, we would add a GetCalorie method to the ICalorieService interface
                var goal = _calorieService.GetGoal(userId);
                var food = _calorieService.GetFood(userId);
                var exercise = _calorieService.GetExercise(userId);

                // Create a new Calorie object with the retrieved values
                var calorie = new Calorie
                {
                    U_Id = userId,
                    DailyIntake = goal,
                    CaloriesConsumed = food,
                    CaloriesBurned = exercise,
                    Today = DateTime.Now
                };

                return calorie;
            }
            catch (Exception ex)
            {
                return NotFound($"Error retrieving calorie data: {ex.Message}");
            }
        }
    }
}
