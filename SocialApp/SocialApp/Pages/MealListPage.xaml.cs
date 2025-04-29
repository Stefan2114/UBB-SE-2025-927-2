using MealPlannerProject.Services;
using MealPlannerProject.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace MealPlannerProject.Pages
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