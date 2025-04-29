// <copyright file="WelcomeViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MealPlannerProject.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using MealPlannerProject.Pages;
    using MealPlannerProject.Services;

    public partial class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            this.GetStartedCommand = new RelayCommand(this.OnGetStarted);
            this.Items = new ObservableCollection<string>();
        }

        public ICommand GetStartedCommand { get; }

        public ObservableCollection<string> Items { get; }

        private void OnGetStarted()
        {
            NavigationService.Instance.NavigateTo(typeof(UserPage));
        }
    }
}
