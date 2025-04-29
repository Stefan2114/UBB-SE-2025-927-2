namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using Microsoft.UI.Xaml.Controls;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            try
            {
                this.InitializeComponent();
                Debug.WriteLine("MainPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in MainPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }
    }
}
