namespace MealPlannerProject.Models
{
    using System.Collections.ObjectModel;

    public class SectionModel
    {
        public SectionModel()
        {
            this.Title = string.Empty;
        }

        public SectionModel(string title)
        {
            this.Title = title;
        }

        public string Title { get; set; }

        public ObservableCollection<GroceryIngredient> Items { get; set; } = new ();
    }
}
