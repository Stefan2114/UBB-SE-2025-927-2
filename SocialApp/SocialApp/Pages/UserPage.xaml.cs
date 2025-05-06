namespace SocialApp.Pages
{
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.ViewModels;
    using System;

    public sealed partial class UserPage : Page
    {
        public string username { get; set; } = string.Empty;



        private Services.UserPageService userPageService = new Services.UserPageService();

        public UserPage()
        {
            this.InitializeComponent();
        }


        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.username = this.FirstNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(this.username))
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
            int userId = this.userPageService.UserHasAnAccount(this.username);

            if (userId != -1)
            {
                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                userId = userPageService.InsertNewUser(this.username);
                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                this.Frame.Navigate(typeof(BodyMetricsPage), this);
            }
        }
    }
}