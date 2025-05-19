using System.Data.SqlClient;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;

namespace AppCommonClasses.Repos
{
    /// <summary>
    /// Repository for meal data access.
    /// </summary>
    public class MealRepository : IMealRepository
    {
        private readonly IDataLink dataLink;

        /// <summary>
        /// Initializes a new instance of the <see cref="MealRepository"/> class.
        /// </summary>
        /// <param name="dataLink">The data link dependency.</param>
        public MealRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        [Obsolete]
        public MealRepository()
        {
            // this.dataLink = new DataLink();
        }

        [Obsolete]
        public async Task<int> CreateMealAsync(Meal meal, int cookingSkillId, int mealTypeId)
        {
            string query = @"INSERT INTO meals (m_name, recipe, cs_id, mt_id, preparation_time, servings, protein, calories, carbohydrates, fat, fiber, sugar, photo_link) 
                               VALUES (@m_name, @recipe, @cs_id, @mt_id, @preparation_time, @servings, @protein, @calories, @carbohydrates, @fat, @fiber, @sugar, @photo_link); 
                               SELECT SCOPE_IDENTITY();";

            var parameters = new SqlParameter[]
            {
                new ("@m_name", meal.Name),
                new ("@recipe", meal.Recipe ?? "No directions provided"),
                new ("@cs_id", cookingSkillId),
                new ("@mt_id", mealTypeId),
                new ("@preparation_time", meal.PreparationTime),
                new ("@servings", meal.Servings),
                new ("@protein", meal.Protein),
                new ("@calories", meal.Calories),
                new ("@carbohydrates", meal.Carbohydrates),
                new ("@fat", meal.Fat),
                new ("@fiber", meal.Fiber),
                new ("@sugar", meal.Sugar),
                new ("@photo_link", meal.PhotoLink ?? "default.jpg"),
            };

            return await Task.FromResult(dataLink.ExecuteScalar<int>(query, parameters, false));
        }

        [Obsolete]
        public async Task<int> AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var parameters = new SqlParameter[]
        {
                new ("@mealId", mealId),
                new ("@ingredientId", ingredientId),
                new ("@quantity", quantity),
            };

            int result = dataLink.ExecuteNonQuery("InsertMealIngredient", parameters);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Retrieves all meals from the database.
        /// </summary>
        /// <returns>A list of Meal objects.</returns>
        public async Task<List<Meal>> GetAllMealsAsync()
        {
            string query = "SELECT * FROM meals";
            var dataTable = dataLink.ExecuteSqlQuery(query, null);

            var meals = new List<Meal>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                meals.Add(new Meal
                {
                    Name = row["m_name"]?.ToString(),
                    Recipe = row["recipe"]?.ToString(),
                    PreparationTime = row["preparation_time"] != DBNull.Value ? Convert.ToInt32(row["preparation_time"]) : 0,
                    Servings = row["servings"] != DBNull.Value ? Convert.ToInt32(row["servings"]) : 0,
                    Protein = row["protein"] != DBNull.Value ? Convert.ToInt32(row["protein"]) : 0,
                    Calories = row["calories"] != DBNull.Value ? Convert.ToInt32(row["calories"]) : 0,
                    Carbohydrates = row["carbohydrates"] != DBNull.Value ? Convert.ToInt32(row["carbohydrates"]) : 0,
                    Fat = row["fat"] != DBNull.Value ? Convert.ToInt32(row["fat"]) : 0,
                    Fiber = row["fiber"] != DBNull.Value ? Convert.ToInt32(row["fiber"]) : 0,
                    Sugar = row["sugar"] != DBNull.Value ? Convert.ToInt32(row["sugar"]) : 0,
                    PhotoLink = row["photo_link"]?.ToString()
                });
            }

            return await Task.FromResult(meals);
        }
    }
}
