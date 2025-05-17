namespace AppCommonClasses.Repos
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using AppCommonClasses.Data;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;

    public class GroceryListRepository : IGroceryListRepository
    {
        private readonly SocialAppDbContext dbContext;

        public GroceryListRepository(SocialAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GroceryIngredient>> GetIngredientsForUser(long userId)
        {
            var ingredients = await dbContext.GroceryIngredients.Where(i => i.Id == userId).ToListAsync();
            foreach (var ingredient in ingredients)
            {
                var ing = await dbContext.Ingredient.Where(i => i.Id == ingredient.IngredientId).FirstOrDefaultAsync();
                ingredient.Name = ing != null ? ing.Name : "No name";
                ingredient.Id = ing != null ? userId : -1;
            }

            return ingredients;
        }

        public async Task UpdateIsChecked(long userId, int ingredientId, bool isChecked)
        {
            GroceryIngredient? toUpdate = await dbContext.GroceryIngredients.FirstOrDefaultAsync(item => item.Id == userId && item.IngredientId == ingredientId);

            if (toUpdate != null)
            {
                toUpdate.IsChecked = isChecked;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<GroceryIngredient> AddIngredientToUser(long userId, GroceryIngredient ingredient)
        {
            Ingredient? result = await dbContext.Ingredient.FirstOrDefaultAsync(i => i.Name == ingredient.Name);

            if (result == null)
            {
                result = new Ingredient
                {
                    Name = ingredient.Name,
                    Category = "Uncategorized",
                    UserId = userId,
                };
                dbContext.Ingredient.Add(result);
                await dbContext.SaveChangesAsync();
            }


            var groceryItem = new GroceryIngredient
            {
                Id = userId,
                IngredientId = result.Id,
                IsChecked = ingredient.IsChecked,
            };
            dbContext.GroceryIngredients.Add(groceryItem);
            dbContext.SaveChanges();

            return ingredient;
        }
    }
}
