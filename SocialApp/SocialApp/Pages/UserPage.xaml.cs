namespace SocialApp.Pages
{
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.ViewModels;
    using System;

    public sealed partial class UserPage : Page
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        private Services.UserPageService userPageService = new Services.UserPageService();

        public UserPage()
        {
            this.InitializeComponent();
        }

        public string GetUserFullName()
        {
            return $"{this.LastName} {this.FirstName}";
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            this.FirstName = this.FirstNameTextBox.Text.Trim();
            this.LastName = this.LastNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.LastName))
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
            int userId = this.userPageService.UserHasAnAccount(this.LastName + " " + this.FirstName);

            if (userId != -1)
            {
                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                userId = userPageService.InsertNewUser(this.LastName + " " + this.FirstName);
                GroceryViewModel.UserId = userId;
                AddFoodPageViewModel.UserId = userId;
                MainViewModel.UserId = userId;
                this.Frame.Navigate(typeof(BodyMetricsPage), this);
            }
        }
    }
}