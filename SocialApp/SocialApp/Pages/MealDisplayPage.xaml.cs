using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialApp.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialApp.Pages
{
    public sealed partial class MealDisplayPage : Page
    {
        private MealViewModel _viewModel;
        public MealViewModel ViewModel
        {
            get => _viewModel;
            private set
            {
                _viewModel = value;
                DataContext = value;
            }
        }

        public MealDisplayPage()
        {
            this.InitializeComponent();
            ViewModel = new MealViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is MealViewModel meal)
            {
                ViewModel = meal;
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void NavigateToMealListPage(object sender, RoutedEventArgs e)
        {
            if (Frame != null)
            {
                Frame.Navigate(typeof(MealListPage));
            }
        }
    }
}
