namespace SocialApp.Services
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using SocialApp.Interfaces;

    public class GroceryListService : IGroceryListService
    {
        private IGroceryListRepository groceryRepository;

        public GroceryListService(IGroceryListRepository groceryRepository)
        {
            this.groceryRepository = groceryRepository;
        }

        public async Task<List<GroceryIngredient>> GetIngredientsForUser(long userId)
        {
            var ingredients = await this.groceryRepository.GetIngredientsForUser(userId);
            return ingredients;
        }

        public async Task UpdateIsChecked(long userId, int ingredientId, bool isChecked)
        {
            await this.groceryRepository.UpdateIsChecked(userId, ingredientId, isChecked);
        }

        public async Task<GroceryIngredient> AddIngredientToUser(long userId, GroceryIngredient ingredient, string newGroceryIngredientName, ObservableCollection<SectionModel> sections)
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

            return await this.groceryRepository.AddIngredientToUser(userId, ingredient);
        }
    }
}
