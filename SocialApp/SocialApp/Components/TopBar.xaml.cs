using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SocialApp.Pages;
using SocialApp.Windows;


namespace SocialApp.Components
{
    public sealed partial class TopBar : UserControl
    {
        public TopBar()
        {
            this.InitializeComponent();
        }

        private async void SetPhoto()
        {
            var controller = App.Services.GetService<AppController>();
            if (controller?.CurrentUser != null && !string.IsNullOrEmpty(controller.CurrentUser.Image))
            {
                UserImage.Source = await AppController.DecodeBase64ToImageAsync(controller.CurrentUser.Image);
            }
        }

        private void SetNavigationButtons()
        {
            HomeButton.Click += HomeClick;
            UserButton.Click += UserClick;
            GroupsButton.Click += GroupsClick;
            CreatePostButton.Click += CreatePostButton_Click;
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            HomeButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Blue);
            GroupsButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            
            // Use App.NavigationController for navigation
            App.NavigationController.NavigateTo(typeof(HomeScreen));
        }

        private void GroupsClick(object sender, RoutedEventArgs e)
        {
            if (IsLoggedIn())
            {
                HomeButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
                GroupsButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Blue);
                CreatePostButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
                
                App.NavigationController.NavigateTo(typeof(GroupsScreen));
            }
            else
            {
                HomeButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
                GroupsButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
                CreatePostButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
                
                App.NavigationController.NavigateTo(typeof(LoginRegisterPage));
            }
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            if (IsLoggedIn())
            {
                App.NavigationController.NavigateTo(typeof(UserPage));
            }
            else
            {
                App.NavigationController.NavigateTo(typeof(LoginRegisterPage));
            }
        }

        private void CreatePostButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoggedIn())
            {
                App.NavigationController.NavigateTo(typeof(CreatePost));
            }
            else
            {
                App.NavigationController.NavigateTo(typeof(LoginRegisterPage));
            }
        }

        private bool IsLoggedIn()
        {
            var controller = App.Services.GetService<AppController>();
            return controller?.CurrentUser != null;
        }

        public void SetHome()
        {
            HomeButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Blue);
            GroupsButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
        }
        
        public void SetGroups()
        {
            HomeButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            GroupsButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Blue);
            CreatePostButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
        }
        
        public void SetCreate()
        {
            HomeButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            GroupsButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Blue);
        }
        
        public void SetNone()
        {
            HomeButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            GroupsButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new SolidColorBrush(Microsoft.UI.Colors.White);
        }

        public Button HomeButtonInstance => HomeButton;
        public Button GroupsButtonInstance => GroupsButton;
        public Button CreatePostButtonInstance => CreatePostButton;
        public Button UserButtonInstance => UserButton;
    }
}
