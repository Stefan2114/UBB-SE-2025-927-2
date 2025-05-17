namespace SocialApp.Repository
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class IngredientRepository : IIngredientRepository
    {
        private readonly IDataLink dataLink;

        // Constructor with Dependency Injection for IDataLink
        public IngredientRepository()
        {
            this.dataLink = dataLink;
        }

        public IngredientRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        // Async method to fetch ingredient details by name
        [Obsolete]
        public async Task<Ingredient?> GetIngredientByNameAsync(string name)
        {
            const string query = @"SELECT i_id, i_name, calories, protein, carbohydrates, fat, fiber, sugar 
                                   FROM ingredients 
                                   WHERE i_name = @name;";

            var parameters = new SqlParameter[] { new("@name", name) };

            // Execute SQL query asynchronously
            DataTable result = await Task.Run(() => dataLink.ExecuteSqlQuery(query, parameters));

            if (result.Rows.Count > 0)
            {
                var row = result.Rows[0];
                return new Ingredient
                {
                    Id = Convert.ToInt32(row["i_id"]),
                    UserId = 0, // Assuming UserId is not provided in the query, defaulting to 0
                    Name = row["i_name"].ToString(),
                    Category = string.Empty, // Assuming Category is not provided in the query, defaulting to an empty string
                    Calories = row["calories"] == DBNull.Value ? 0 : Convert.ToDouble(row["calories"]),
                    Protein = row["protein"] == DBNull.Value ? 0 : Convert.ToDouble(row["protein"]),
                    Carbs = row["carbohydrates"] == DBNull.Value ? 0 : Convert.ToDouble(row["carbohydrates"]),
                    Fats = row["fat"] == DBNull.Value ? 0 : Convert.ToDouble(row["fat"]),
                    Fiber = row["fiber"] == DBNull.Value ? 0 : Convert.ToDouble(row["fiber"]),
                    Sugar = row["sugar"] == DBNull.Value ? 0 : Convert.ToDouble(row["sugar"])
                };


            }

            return Ingredient.NoIngredient;
        }
    }
}
