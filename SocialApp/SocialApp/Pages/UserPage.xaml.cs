namespace SocialApp.Pages
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.Interfaces;
    using SocialApp.Proxies;
    using SocialApp.Repository;
    using SocialApp.Services;
    using SocialApp.ViewModels;
    using System;

    public sealed partial class UserPage : Page
    {
        public string Username { get; set; } = string.Empty;
        private IUserService userService;

        private Services.UserPageService userPageService = new Services.UserPageService();

        public UserPage()
        {
            this.InitializeComponent();
            var repo = new UserRepositoryProxy();
            this.userService = new UserService(repo);
        }


        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.Username = this.UsernameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(this.Username))
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Please enter both your first and last name.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot,
                };
                _ = dialog.ShowAsync();
                return;
            }
            int userId = this.userPageService.UserHasAnAccount(this.Username);


            if (userId != -1)
            {
                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                App.Services.GetService<AppController>().CurrentUser = this.userService.GetById(userId);
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                userId = userPageService.InsertNewUser(this.Username);

                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                App.Services.GetService<AppController>().CurrentUser = this.userService.GetById(userId);
                this.Frame.Navigate(typeof(BodyMetricsPage), this);
            }
        }
    }
}