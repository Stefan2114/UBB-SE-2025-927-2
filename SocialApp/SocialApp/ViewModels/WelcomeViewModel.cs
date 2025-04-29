// <copyright file="WelcomeViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SocialApp.ViewModels
{
    using SocialApp.Services;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public partial class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            GetStartedCommand = new RelayCommand(OnGetStarted);
            Items = new ObservableCollection<string>();
        }

        public ICommand GetStartedCommand { get; }

        public ObservableCollection<string> Items { get; }

        private void OnGetStarted()
        {
            NavigationService.Instance.NavigateTo(typeof(UserPage));
        }
    }
}
