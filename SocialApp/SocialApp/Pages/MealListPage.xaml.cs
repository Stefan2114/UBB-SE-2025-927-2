using AppCommonClasses.Interfaces;
using Microsoft.UI.Xaml.Controls;
using SocialApp.Proxies;
using SocialApp.Services;
using SocialApp.ViewModels;

namespace SocialApp.Pages
{
    public sealed partial class MealListPage : Page
    {
        public MealListPage()
        {
            this.InitializeComponent();

            this.DataContext = new MealListViewModel();
        }
    }
}