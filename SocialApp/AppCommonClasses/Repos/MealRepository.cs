// <copyright file="MealRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AppCommonClasses.Repos
{
    using System.Collections.Generic;
    using System.Linq;
    using AppCommonClasses.Data;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;

    /// <summary>
    /// Repository for managing meals.
    /// </summary>
    public class MealRepository : IMealRepository
    {
        private readonly SocialAppDbContext dbContext;

        public MealRepository(SocialAppDbContext context)
        {
            dbContext = context;
        }

        public void AddMealIngredientAsync(int mealId, int ingredientId, float quantity)
        {
            var mealIngredient = new MealIngredient
            {
                MealId = mealId,
                IngredientId = ingredientId,
                Quantity = quantity,
            };

            dbContext.MealIngredients.Add(mealIngredient);
            dbContext.SaveChanges();
        }

        public void CreateMealAsync(Meal meal)
        {
            dbContext.Meals.Add(meal);
            dbContext.SaveChanges();
        }

        public List<Meal> GetAllMeals()
        {
            return dbContext.Meals.ToList();
        }
    }
}
