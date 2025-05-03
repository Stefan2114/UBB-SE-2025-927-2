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
        private IGroceryListRepository gR;

        public GroceryListService(IGroceryListRepository groceryRepository, IGroceryListRepository gR)
        {
            this.groceryRepository = groceryRepository;
            this.gR = gR;
        }

        public async Task<List<GroceryIngredient>> GetIngredientsForUser(int userId)
        {
            var ingredients = await this.groceryRepository.GetIngredientsForUser(userId);
            foreach (var ingredient in ingredients)
            {
                ingredient.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(GroceryIngredient.IsChecked))
                    {
                        var item = s as GroceryIngredient ?? GroceryIngredient.defaultIngredient;
                        _ = this.UpdateIsChecked(userId, item.Id, item.IsChecked);
                    }
                };
            }

            return ingredients;
        }

        public async Task UpdateIsChecked(int userId, int ingredientId, bool isChecked)
        {
            await this.groceryRepository.UpdateIsChecked(userId, ingredientId, isChecked);
        }

        public async Task<GroceryIngredient> AddIngredientToUser(int userId, GroceryIngredient ingredient, string newGroceryIngredientName, ObservableCollection<SectionModel> sections)
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

            // GroceryIngredient r = await this.gR.AddIngredientToUser(userId, ingredient);
            return await this.groceryRepository.AddIngredientToUser(userId, r);
        }
    }
}
