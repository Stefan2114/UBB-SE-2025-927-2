using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialApp.Pages;
using SocialApp.Windows;


namespace SocialApp.Components
{
    public sealed partial class TopBar : UserControl
    {
        private Frame frame;

        public TopBar()
        {
            this.InitializeComponent();
        }

        public void SetFrame(Frame frame)
        {
            this.frame = frame;
            SetPhoto();
            SetNavigationButtons();
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
            HomeButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Blue);
            GroupsButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            frame.Navigate(typeof(HomeScreen));
        }

        private void GroupsClick(object sender, RoutedEventArgs e)
        {

            HomeButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            GroupsButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Blue);
            CreatePostButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            frame.Navigate(typeof(GroupsScreen));

        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(UserPage));

        }

        private void CreatePostButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(CreatePost));
        }


        public void SetHome()
        {
            HomeButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Blue);
            GroupsButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
        }
        public void SetGroups()
        {
            HomeButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            GroupsButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Blue);
            CreatePostButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
        }
        public void SetCreate()
        {
            HomeButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            GroupsButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Blue);
        }
        public void SetNone()
        {
            HomeButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            GroupsButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
            CreatePostButton.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White);
        }

        public Button HomeButtonInstance => HomeButton;
        public Button GroupsButtonInstance => GroupsButton;
        public Button CreatePostButtonInstance => CreatePostButton;
        public Button UserButtonInstance => UserButton;
    }
}
