using MealPlannerProject.Models;
using MealPlannerProject.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using MealPlannerProject.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlannerProject.Services
{
    public class GroceryListService: IGroceryListService
    {
        // No dependency injection for IDataLink, using DataLink directly
        public List<GroceryIngredient> GetIngredientsForUser(int userId)
        {
            var ingredients = new List<GroceryIngredient>();

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId }
            };

            // Using DataLink directly for executing queries
            DataTable table = DataLink.Instance.ExecuteReader("sp_GetUserGroceryList", parameters);

            foreach (DataRow row in table.Rows)
            {
                ingredients.Add(new GroceryIngredient
                {
                    Id = (int)row["i_id"],
                    Name = row["i_name"].ToString(),
                    IsChecked = (bool)row["is_checked"]
                });
            }

            foreach (var ingredient in ingredients)
            {
                ingredient.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                    {
                        var item = (GroceryIngredient)s;
                        this.UpdateIsChecked(userId, item.Id, item.IsChecked);
                    }
                };
            }

            return ingredients;
        }

        public void UpdateIsChecked(int userId, int ingredientId, bool isChecked)
        {
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId },
                new SqlParameter("@IngredientId", SqlDbType.Int) { Value = ingredientId },
                new SqlParameter("@IsChecked", SqlDbType.Bit) { Value = isChecked }
            };

            DataLink.Instance.ExecuteNonQuery("sp_UpdateGroceryItemChecked", parameters);
        }

        public GroceryIngredient AddIngredientToUser(int userId, GroceryIngredient ingredient, string newGroceryIngredientName, ObservableCollection<SectionModel> sections)
        {
            if (ingredient == GroceryIngredient.defaultIngredient)
            {
                if (string.IsNullOrWhiteSpace(newGroceryIngredientName))
                {
                    return GroceryIngredient.defaultIngredient;
                }

                ingredient = new GroceryIngredient
                {
                    Name = newGroceryIngredientName,
                    IsChecked = false,
                };
            }

            if (sections.SelectMany(s => s.Items).Any(i => i.Name == ingredient.Name))
            {
                return GroceryIngredient.defaultIngredient;
            }

            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = userId },
                new SqlParameter("@IngredientName", SqlDbType.NVarChar) { Value = ingredient.Name }
            };

            int newId = DataLink.Instance.ExecuteScalar<int>("sp_AddIngredientToUserList", parameters, true);
            ingredient.Id = newId;

            return ingredient;
        }
    }
}
