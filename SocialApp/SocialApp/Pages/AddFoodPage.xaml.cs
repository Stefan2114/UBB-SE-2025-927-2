namespace MealPlannerProject.Pages
{
    using System;
    using System.Diagnostics;
    using MealPlannerProject.Models;
    using MealPlannerProject.ViewModels;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    public sealed partial class AddFoodPage : Page
    {
        public AddFoodPageViewModel AddFoodPageViewModel { get; } = new AddFoodPageViewModel(); // Initialize ViewModel

        public AddFoodPage()
        {
            try
            {
                this.InitializeComponent();

                this.DataContext = this.AddFoodPageViewModel;
                Debug.WriteLine("AddFoodPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in AddFoodPage constructor: {ex.Message}");
                throw; // Re-throw the exception to be caught by the global handler
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string selectedMeal)
            {
                this.AddFoodPageViewModel.SetCategoryHighlight(selectedMeal);
            }
        }


        private void ServingSizeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // This method will be called when the selection changes in the ListBox
            if (this.ServingSizeListBox.SelectedItem is ServingUnitModel selectedUnit)
            {
                // Update the SelectedUnit in the ViewModel via the ViewModel's property
                this.AddFoodPageViewModel.SelectedUnit = selectedUnit.UnitName;
            }
        }
    }
}