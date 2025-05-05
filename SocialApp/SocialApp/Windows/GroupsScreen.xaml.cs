using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialApp.Components;
using SocialApp.Pages;

namespace SocialApp.Windows
{
    public sealed partial class GroupsScreen : Page
    {

        public GroupsScreen()
        {
            this.InitializeComponent();
            GroupsDrawer.NavigationFrame = this.Frame;
            SetNavigation();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var controller = App.Services.GetService<AppController>();
            App.NavigationController.NavigateTo(typeof(GroupsScreen));
            TopBar.SetGroups();
            GroupsDrawer.NavigationFrame = this.Frame;
        }

        private void SetNavigation()
        {
            TopBar.HomeButtonInstance.Click += HomeClick;
            TopBar.UserButtonInstance.Click += UserClick;
            TopBar.GroupsButtonInstance.Click += GroupsClick;
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomeScreen));
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserPage));
        }

        private void GroupsClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GroupsScreen));
        }
    }
}