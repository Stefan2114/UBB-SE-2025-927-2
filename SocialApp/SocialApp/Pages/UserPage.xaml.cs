namespace SocialApp.Pages
{
    using System;
    using AppCommonClasses.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.Proxies;
    using SocialApp.ViewModels;

    public sealed partial class UserPage : Page
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        private readonly IUserService userService;

        private Services.UserPageService userPageService = new Services.UserPageService();

        public UserPage()
        {
            this.InitializeComponent();
            var repo = new UserServiceProxy();
            this.userService = repo;
        }


        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.Username = this.UsernameTextBox.Text.Trim();
            this.Password = this.PasswordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(this.Username) || string.IsNullOrEmpty(this.Password))
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Please enter both your name and your password.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot,
                };
                _ = dialog.ShowAsync();
                return;
            }

            int userId = this.userPageService.UserHasAnAccount(this.Username);

            if (userId != -1)
            {
                int id = this.userService.Login(this.Username, this.Password);
                if (id == -1)
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "The password is incorrect",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot,
                    };
                    _ = dialog.ShowAsync();
                    return;
                }

                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                App.Services.GetService<AppController>().CurrentUser = this.userService.GetById(userId);
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                userId = this.userPageService.InsertNewUser(this.Username, this.Password);

                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                App.Services.GetService<AppController>().CurrentUser = this.userService.GetById(userId);
                this.Frame.Navigate(typeof(BodyMetricsPage), this);
            }
        }
    }
}