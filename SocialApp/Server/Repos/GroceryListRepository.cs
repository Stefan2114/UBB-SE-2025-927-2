namespace Server.Repos
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;
    using Server.Data;

    public class GroceryListRepository : IGroceryListRepository
    {
        private readonly SocialAppDbContext dbContext;

        public GroceryListRepository(SocialAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GroceryIngredient>> GetIngredientsForUser(int userId)
        {
            var ingredients = await this.dbContext.GroceryIngredients.Where(i => i.Id == userId).ToListAsync();
            return ingredients;
        }

        public async Task UpdateIsChecked(int userId, int ingredientId, bool isChecked)
        {
            GroceryIngredient? toUpdate = await this.dbContext.GroceryIngredients.FirstOrDefaultAsync(item => item.Id == userId && item.IngredientId == ingredientId);

            if (toUpdate != null)
            {
                toUpdate.IsChecked = isChecked;
                this.dbContext.SaveChanges();
            }
        }

        public async Task<GroceryIngredient> AddIngredientToUser(int userId, GroceryIngredient ingredient)
        {
            Ingredient? result = await this.dbContext.Ingredient.FirstOrDefaultAsync(i => i.Name == ingredient.Name);

            if (result == null)
            {
                result = new Ingredient
                {
                    Name = ingredient.Name,
                    Category = "Uncategorized",
                };
                this.dbContext.Ingredient.Add(result);
                await this.dbContext.SaveChangesAsync();
            }

            bool exists = this.dbContext.Ingredient.Any(gl => gl.UserId == userId && gl.Id == ingredient.Id);

            if (!exists)
            {
                var groceryItem = new GroceryIngredient
                {
                    Id = userId,
                    IngredientId = ingredient.IngredientId,
                    IsChecked = false,
                };
                this.dbContext.GroceryIngredients.Add(groceryItem);
                this.dbContext.SaveChanges();
            }

            return ingredient;
        }
    }
}
