namespace Server.DTOs
{
    using System.Collections.ObjectModel;
    using AppCommonClasses.Models;

    public class GroceryListDTO
    {
        public GroceryIngredient Ingredient { get; set; }

        public string NewIngredientName { get; set; }

        public ObservableCollection<SectionModel> Sections { get; set; }
    }
}
