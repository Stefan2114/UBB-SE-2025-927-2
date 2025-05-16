namespace SocialApp.Services
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.Interfaces;
    using SocialApp.ViewModels;

    public class NavigationService : INavigationService
    {
        private static NavigationService? instance;
        private Frame? mainFrame;

        public static NavigationService Instance => instance ??= new NavigationService();

        public void Initialize(Frame mainFrame)
        {
            this.mainFrame = mainFrame;
        }

        public void NavigateTo(Type pageType, object? parameter = null)
        {
            mainFrame?.Navigate(pageType, parameter);
        }

        public void NavigateTo<TPage>()
            where TPage : Page
        {
            var page = App.Services.GetRequiredService<TPage>();
            mainFrame.Content = page;
        }

        public void GoBack()
        {
            if (mainFrame?.CanGoBack == true)
            {
                mainFrame.GoBack();
            }
        }

        internal void NavigateTo(Type type, BodyMetricsViewModel bodyMetricsViewModel)
        {
            mainFrame?.Navigate(type, bodyMetricsViewModel);
        }

        internal void NavigateTo(Type type, GoalPageViewModel goalPageViewModel)
        {
            mainFrame?.Navigate(type, goalPageViewModel);
        }

        internal void NavigateTo(Type type, ActivityLevelViewModel activityLevelViewModel)
        {
            mainFrame?.Navigate(type, activityLevelViewModel);
        }

        internal void NavigateTo(Type type, GroceryViewModel groceryViewModel)
        {
            mainFrame?.Navigate(type, groceryViewModel);
        }

        internal void NavigateTo(Type type, CookingLevelViewModel cookingLevelViewModel)
        {
            // throw new NotImplementedException();
            mainFrame?.Navigate(type, cookingLevelViewModel);
        }
    }
}
