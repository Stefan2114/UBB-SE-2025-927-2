namespace SocialApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using NUnit.Framework;
    using SocialApp.Interfaces;
    using SocialApp.Proxies;
    using SocialApp.Queries;
    using SocialApp.Repository;

    public class MealService : IMealService
    {
        private readonly IMealRepository mealRepoProxy;
        private readonly IngredientRepository ingredientDatabaseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MealService"/> class.
        /// </summary>
        [Obsolete]
        public MealService()
        {
            this.mealRepoProxy = new MealRepositoryProxy();
            this.ingredientDatabaseRepository = new IngredientRepository();
        }

        public Task<bool> CreateMealWithCookingLevelAsync(Meal mealToCreate, string cookingLevelDescription)
        {
            try
            {
                int cookingSkillIdentifier = ResolveCookingSkillIdentifier(cookingLevelDescription);
                int mealTypeIdentifier = ResolveMealTypeIdentifier(mealToCreate.Category);

                mealToCreate.CookingLevel = cookingLevelDescription;
                this.mealRepoProxy.CreateMealAsync(mealToCreate);
                return Task.FromResult(true);

            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error creating meal: {exception.Message}");
                Debug.WriteLine($"Stack trace: {exception.StackTrace}");
                return Task.FromResult(false);
            }
        }

        public List<Meal> RetrieveAllMealsAsync()
        {
            return this.mealRepoProxy.GetAllMeals();
        }

        public async Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName)
        {
            return await Task.Run(() => ingredientDatabaseRepository.GetIngredientByNameAsync(ingredientName));
        }

        [Obsolete]
        public Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity)
        {
            this.mealRepoProxy.AddMealIngredientAsync(mealIdentifier, ingredientIdentifier, ingredientQuantity);
            return Task.FromResult(true);
        }

        private int ResolveMealTypeIdentifier(string mealCategory)
        {
            return mealCategory?.ToLower() switch
            {
                "breakfast" => (int)MealModel.BreakfastTypeId,
                "lunch" => (int)MealModel.LunchTypeId,
                "dinner" => (int)MealModel.DinnerTypeId,
                "snack" => (int)MealModel.SnackTypeId,
                "dessert" => (int)MealModel.DessertTypeId,
                "post-workout" => (int)MealModel.PostWorkoutTypeId,
                "pre-workout" => (int)MealModel.PreWorkoutTypeId,
                "vegan meal" => (int)MealModel.VeganMealTypeId,
                "high-protein meal" => (int)MealModel.HighProteinMealTypeId,
                "low-carb meal" => (int)MealModel.LowCarbMealTypeId,
                _ => (int)MealModel.DefaultMealTypeId
            };
        }

        private int ResolveCookingSkillIdentifier(string cookingSkillLevel)
        {
            Debug.WriteLine($"ResolveCookingSkillIdentifier called with cooking skill level: {cookingSkillLevel}");
            return cookingSkillLevel?.ToLower() switch
            {
                "beginner" => (int)MealModel.BeginnerSkillId,
                "intermediate" => (int)MealModel.IntermediateSkillId,
                "advanced" => (int)MealModel.AdvancedSkillId,
                _ => (int)MealModel.DefaultCookingSkillId
            };
        }

        public Task<int> CreateMealAsync(Meal mealToCreate)
        {
            this.mealRepoProxy.CreateMealAsync(mealToCreate);
            return Task.FromResult(1);
        }
    }
}