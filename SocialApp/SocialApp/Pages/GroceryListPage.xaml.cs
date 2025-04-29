// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MealPlannerProject.Pages
{
    using AppCommonClasses.Models;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;

    public sealed partial class GroceryListPage : Page
    {
        public GroceryViewModel ViewModel { get; } = new ();

        public GroceryListPage() => this.InitializeComponent();

        [System.Obsolete]
        private void AddGroceryIngredient_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.DataContext is GroceryIngredient selectedIngredient)
            {
                this.ViewModel.AddGroceryIngredient(selectedIngredient);
            }
        }

        private void NavigateToMealDisplay(object sender, RoutedEventArgs e)
        {
            this.Frame?.Navigate(typeof(MainPage));
        }
    }
}