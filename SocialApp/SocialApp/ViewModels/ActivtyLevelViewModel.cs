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
        private string firstName;
        private string lastName;

        private IActivityPageService activityPageService = new ActivityPageService();

        public ObservableCollection<string> ActivityLevels { get; } = ActivityPageService.GetActivityLevels();

        public ICommand BackCommand { get; }

        public ICommand NextCommand { get; }

        [System.Obsolete]
        public ActivityLevelViewModel()
        {
            selectedActivity = string.Empty;
            firstName = string.Empty;
            lastName = string.Empty;
            BackCommand = new RelayCommand(GoBack);
            NextCommand = new RelayCommand(GoNext);
        }

        private void GoBack()
        {
            App.NavigationController.GoBack();
        }

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

        public string SelectedActivity
        {
            get => selectedActivity;
            set
            {
                selectedActivity = value;
                OnPropertyChanged(nameof(SelectedActivity));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        [System.Obsolete]
        public void GoNext()
        {
            activityPageService.AddActivity(FirstName, LastName, SelectedActivity);
            App.NavigationController.NavigateTo(typeof(CookingLevelPage), this);
        }

        public new event PropertyChangedEventHandler? PropertyChanged;

        protected new void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
