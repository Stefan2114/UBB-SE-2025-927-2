namespace SocialApp.ViewModels
{
    using SocialApp.Interfaces;
    using SocialApp.Pages;
    using SocialApp.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public class ActivityLevelViewModel : ViewModelBase
    {
        private string selectedActivity;
        private string username;

        private IActivityPageService activityPageService = new ActivityPageService();

        public ObservableCollection<string> ActivityLevels { get; } = ActivityPageService.GetActivityLevels();

        public ICommand BackCommand { get; }

        public ICommand NextCommand { get; }

        [System.Obsolete]
        public ActivityLevelViewModel()
        {
            selectedActivity = string.Empty;
            username = string.Empty;
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
        }

        private void GoBack()
        {
            NavigationService.Instance.GoBack();
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        public string SelectedActivity
        {
            get => selectedActivity;
            set
            {
                selectedActivity = value;
                OnPropertyChanged(nameof(SelectedActivity));
            }
        }

        public void SetUserInfo(string username)
        {
            Username = username;
        }

        [System.Obsolete]
        public void GoNext()
        {
            activityPageService.AddActivity(Username, SelectedActivity);
            NavigationService.Instance.NavigateTo(typeof(CookingLevelPage), this);
        }

        public new event PropertyChangedEventHandler? PropertyChanged;

        protected new void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
