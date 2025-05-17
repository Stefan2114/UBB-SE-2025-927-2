namespace AppCommonClasses.Services
{
    using AppCommonClasses.Models;
    using AppCommonClasses.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class MealService : IMealService
    {
        private readonly IMealRepository mealRepository;
        private readonly IIngredientRepository ingredientRepository;

        public MealService(IMealRepository mealRepository, IIngredientRepository ingredientRepository)
        {
            this.mealRepository = mealRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public async Task<bool> CreateMealWithCookingLevelAsync(Meal mealToCreate, string cookingLevelDescription)
        {
            try
            {
                int cookingSkillIdentifier = ResolveCookingSkillIdentifier(cookingLevelDescription);
                int mealTypeIdentifier = ResolveMealTypeIdentifier(mealToCreate.Category);

                int createdMealIdentifier = await mealRepository.CreateMealAsync(
                    mealToCreate,
                    cookingSkillIdentifier,
                    mealTypeIdentifier);

                return createdMealIdentifier > 0;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error creating meal: {exception.Message}");
                return false;
            }
        }

        public async Task<List<Meal>> RetrieveAllMealsAsync()
        {
            return await mealRepository.GetAllMealsAsync();
        }

        public async Task<Ingredient?> RetrieveIngredientByNameAsync(string ingredientName)
        {
            return await ingredientRepository.GetIngredientByNameAsync(ingredientName);
        }

        public async Task<int> CreateMealAsync(Meal mealToCreate)
        {
            try
            {
                int cookingSkillIdentifier = ResolveCookingSkillIdentifier(mealToCreate.CookingLevel);
                int mealTypeIdentifier = ResolveMealTypeIdentifier(mealToCreate.Category);

                return await mealRepository.CreateMealAsync(
                    mealToCreate,
                    cookingSkillIdentifier,
                    mealTypeIdentifier);
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error creating meal: {exception.Message}");
                return -1;
            }
        }

        public async Task<bool> AddIngredientToMealAsync(int mealIdentifier, int ingredientIdentifier, float ingredientQuantity)
        {
            int operationResult = await mealRepository.AddMealIngredientAsync(
                mealIdentifier,
                ingredientIdentifier,
                ingredientQuantity);

            return operationResult > 0;
        }

        private int ResolveMealTypeIdentifier(string mealCategory)
        {
            return mealCategory?.ToLowerInvariant() switch
            {
                "breakfast" => 1,
                "lunch" => 2,
                "dinner" => 3,
                "snack" => 4,
                "dessert" => 5,
                _ => 0 
            };
        }

        private int ResolveCookingSkillIdentifier(string cookingSkillLevel)
        {
            return cookingSkillLevel?.ToLowerInvariant() switch
            {
                "beginner" => 1,
                "easy" => 1,
                "intermediate" => 2,
                "medium" => 2,
                "advanced" => 3,
                "hard" => 3,
                _ => 0 
            };
        }

    }
}
