namespace SocialApp.Interfaces
{
    using Microsoft.UI.Xaml.Controls;
    using SocialApp.ViewModels;
    using System;

    /// <summary>
    /// Interface for the navigation controller that handles navigation between frames.
    /// </summary>
    public interface INavigationController
    {
        /// <summary>
        /// Initializes the navigation controller with the main frame.
        /// </summary>
        /// <param name="frame">The main frame for navigation.</param>
        void Initialize(Frame frame);

        /// <summary>
        /// Navigates to the specified page type.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        /// <param name="parameter">Optional parameter to pass to the page.</param>
        void NavigateTo(Type pageType, object? parameter = null);

        /// <summary>
        /// Navigates back to the previous page if possible.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Checks if navigation back is possible.
        /// </summary>
        /// <returns>True if navigation back is possible; otherwise, false.</returns>
        bool CanGoBack();
    }
}