namespace SocialApp.Pages
{
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;
    using SocialApp.ViewModels;
    using System;
    using System.Diagnostics;

    public sealed partial class CookingLevelPage : Page
    {
        private const string EmptyCookingSkillSelectionError = "Please select a cooking skill level.";

        private readonly CookingLevelViewModel cookingLevelViewModel;

        public CookingLevelPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("CookingLevelPage initialized successfully.");
                this.cookingLevelViewModel = new CookingLevelViewModel();
                this.DataContext = this.cookingLevelViewModel;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Exception in CookingLevelPage constructor: {exception.Message}");
                throw;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs navigationEventArgs)
        {
            base.OnNavigatedTo(navigationEventArgs);

            if (navigationEventArgs.Parameter is ActivityLevelViewModel activityLevelViewModel)
            {
                Debug.WriteLine($"Cooking page received user: {activityLevelViewModel.Username}");
                this.cookingLevelViewModel.SetUserInfo(activityLevelViewModel.Username);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.cookingLevelViewModel.SelectedCookingSkill))
                {
                    this.ShowErrorMessageDialog(EmptyCookingSkillSelectionError);
                    return;
                }

                this.cookingLevelViewModel.NavigateToNextPage();
            }
            catch (Exception exception)
            {
                this.ShowErrorMessageDialog($"An error occurred: {exception.Message}");
            }
        }

        private async void ShowErrorMessageDialog(string errorMessage)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Error",
                Content = errorMessage,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot,
            };

            await errorDialog.ShowAsync();
        }
    }
}
