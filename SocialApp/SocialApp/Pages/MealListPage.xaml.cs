using MealPlannerProject.Services;
using MealPlannerProject.ViewModels;
using Microsoft.UI.Xaml.Controls;

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