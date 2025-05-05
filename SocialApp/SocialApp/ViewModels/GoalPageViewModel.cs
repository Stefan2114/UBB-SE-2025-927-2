namespace SocialApp.ViewModels
{
    using CommunityToolkit.Mvvm.Input;
    using SocialApp.Pages;
    using SocialApp.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    internal class GoalPageViewModel
    {
        public ObservableCollection<string> Goals { get; } = new ObservableCollection<string>
            {
                "Lose weight",
                "Gain weight",
                "Maintain weight",
                "Body recomposition",
                "Improve overall health",
            };

        private string selectedGoal = string.Empty; // Initialize to avoid CS8618

        public string SelectedGoal
        {
            get => selectedGoal;
            set
            {
                selectedGoal = value;
                OnPropertyChanged(nameof(SelectedGoal));
            }
        }

        public ICommand SelectGoalCommand { get; }

        public ICommand BackCommand { get; }

        public ICommand NextCommand { get; }

        [System.Obsolete]
        public GoalPageViewModel()
        {
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
            SelectGoalCommand = new RelayCommand<string>(OnGoalSelected);
        }

        private void OnGoalSelected(string? goal)
        {
            SelectedGoal = goal != null ? goal : string.Empty;
        }

        private void GoBack()
        {
            App.NavigationController.GoBack();
        }

        private GoalPageService goalPageService = new GoalPageService();

        private string firstName = string.Empty;
        private string lastName = string.Empty;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void GoNext()
        {
            goalPageService.AddGoals(FirstName, LastName, SelectedGoal);
            App.NavigationController.NavigateTo(typeof(ActivityLevelPage), this);
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Mark as nullable to avoid CS8618

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
