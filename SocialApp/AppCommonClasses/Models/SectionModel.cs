namespace AppCommonClasses.Models
{
    using System.Collections.ObjectModel;

    public class SectionModel
    {
        public SectionModel()
        {
            Title = string.Empty;
        }

        public SectionModel(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public ObservableCollection<GroceryIngredient> Items { get; set; } = new();
    }
}
