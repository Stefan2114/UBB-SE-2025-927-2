namespace SocialApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using SocialApp.Pages;
    using SocialApp.Proxies;
    using SocialApp.Services;

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
            NavigationService.Instance.GoBack();
        }

        private GoalPageService goalPageService = new GoalPageService(new GoalRepositoryProxy());

        private string username = string.Empty;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public void SetUserInfo(string username)
        {
            this.Username = username;
        }

        public void GoNext()
        {
            this.goalPageService.AddGoals(this.Username, this.SelectedGoal);
            NavigationService.Instance.NavigateTo(typeof(ActivityLevelPage), this);
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Mark as nullable to avoid CS8618

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
