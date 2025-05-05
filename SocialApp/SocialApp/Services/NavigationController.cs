namespace SocialApp.Controllers
{
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.Interfaces;
    using SocialApp.ViewModels;
    using System;

    /// <summary>
    /// Controller for handling navigation between frames in the application.
    /// Provides a centralized way to navigate to different pages with various view models.
    /// </summary>
    public class NavigationController : INavigationController
    {
        private static NavigationController? instance;
        private Frame? mainFrame;

        /// <summary>
        /// Gets the singleton instance of the NavigationController.
        /// </summary>
        public static NavigationController Instance => instance ??= new NavigationController();

        /// <summary>
        /// Private constructor to enforce singleton pattern.
        /// </summary>
        private NavigationController()
        {
        }

        /// <summary>
        /// Initializes the NavigationController with the main frame.
        /// </summary>
        /// <param name="frame">The main frame for navigation.</param>
        public void Initialize(Frame frame)
        {
            mainFrame = frame;
        }

        /// <summary>
        /// Navigates to the specified page type.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        /// <param name="parameter">Optional parameter to pass to the page.</param>
        public void NavigateTo(Type pageType, object? parameter = null)
        {
            mainFrame?.Navigate(pageType, parameter);
        }

        /// <summary>
        /// Navigates back to the previous page if possible.
        /// </summary>
        public void GoBack()
        {
            if (mainFrame?.CanGoBack == true)
            {
                mainFrame.GoBack();
            }
        }

        /// <summary>
        /// Checks if navigation back is possible.
        /// </summary>
        /// <returns>True if navigation back is possible; otherwise, false.</returns>
        public bool CanGoBack()
        {
            return mainFrame?.CanGoBack == true;
        }
    }
}