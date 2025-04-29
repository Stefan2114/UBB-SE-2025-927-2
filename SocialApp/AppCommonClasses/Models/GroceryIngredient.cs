using CommunityToolkit.Mvvm.ComponentModel;

namespace AppCommonClasses.Models
{
    public class GroceryIngredient : ObservableObject
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public readonly static GroceryIngredient defaultIngredient = new GroceryIngredient() { Id = -1, _name = "", IsChecked = false };
    }
}
