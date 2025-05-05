// <copyright file="MealRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Server.Repos
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Server.Data;

    /// <summary>
    /// Repository for managing meals.
    /// </summary>
    public class MealRepository : IMealRepository
    {
        private readonly SocialAppDbContext dbContext;

        public MealRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }

        public void AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var mealIngredient = new MealIngredient
            {
                MealId = mealId,
                IngredientId = ingredientId,
                Quantity = quantity,
            };

            this.dbContext.MealIngredients.Add(mealIngredient);
            this.dbContext.SaveChanges();
        }

        public void CreateMealAsync(Meal meal)
        {
            this.dbContext.Meals.Add(meal);
            this.dbContext.SaveChanges();
        }

        public List<Meal> GetAllMeals()
        {
            return this.dbContext.Meals.ToList();
        }
    }
}
