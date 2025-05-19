namespace AppCommonClasses.Repos
{
    using System.Threading.Tasks;
    using AppCommonClasses.Data;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.EntityFrameworkCore;

    public class IngredientRepository : IIngredientRepository
    {
        private readonly SocialAppDbContext dbContext;

        public IngredientRepository(SocialAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Ingredient GetIngredientByName(string name)
        {
            var ingredient = dbContext.Ingredients
                .AsNoTracking()
                .FirstOrDefault(i => i.Name == name);

            // Optional fallback if not found
            return ingredient ?? Ingredient.NoIngredient;
        }
    }
}
