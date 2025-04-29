using Microsoft.UI.Xaml.Controls;
using SocialApp.Services;
using SocialApp.ViewModels;

namespace SocialApp.Pages
{
    public sealed partial class MealListPage : Page
    {
        public MealListPage()
        {
            this.InitializeComponent();
            this.DataContext = new MealListViewModel(new MealService());
        }
    }
}