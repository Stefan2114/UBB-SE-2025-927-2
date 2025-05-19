using Microsoft.UI.Xaml.Controls;
using SocialApp.ViewModels;

namespace SocialApp.Pages
{
    public sealed partial class MealListPage : Page
    {
        public MealListViewModel viewModel { get; }
        public MealListPage(MealListViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}