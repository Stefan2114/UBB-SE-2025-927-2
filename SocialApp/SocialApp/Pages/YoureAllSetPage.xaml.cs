namespace SocialApp.Pages
{
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;
    using SocialApp.ViewModels;
    using System;
    using System.Diagnostics;

    public sealed partial class YoureAllSetPage : Page
    {
        private YoureAllSetViewModel youreAllSetViewModel = new YoureAllSetViewModel();

        public YoureAllSetPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("YoureAllSetPage initialized successfully.");
                this.DataContext = this.youreAllSetViewModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in YoureAllSetPage constructor: {ex.Message}");
                throw;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is DietaryPreferencesViewModel dpViewModel)
            {
                Debug.WriteLine($"GoalPage received user: {dpViewModel.FirstName} {dpViewModel.LastName}");
                this.youreAllSetViewModel.SetUserInfo(dpViewModel.FirstName, dpViewModel.LastName);
            }
        }
    }
}
