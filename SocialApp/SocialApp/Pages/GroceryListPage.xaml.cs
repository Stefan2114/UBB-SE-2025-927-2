// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialApp.Pages
{
    using AppCommonClasses.Models;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.ViewModels;

    public sealed partial class GroceryListPage : Page
    {
        public GroceryViewModel ViewModel { get; set; }

        public GroceryListPage(GroceryViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.InitializeComponent();
            this.DataContext = this.ViewModel;
        }

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