namespace SocialApp.ViewModels
{
    using SocialApp.Interfaces;
    using SocialApp.Pages;
    using SocialApp.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public partial class CookingLevelViewModel : ViewModelBase
    {
        private readonly ICookingPageService cookingPageService;

        private string username;
        private string userSelectedCookingSkill;

        public ObservableCollection<string> CookingLevels { get; } = new ObservableCollection<string>
        {
            "I am a beginner",
            "I cook sometimes",
            "I love cooking",
            "I prefer quick meals",
            "I meal prep",
        };

        public ICommand NavigateToPreviousPageCommand { get; }

        public ICommand NavigateToNextPageCommand { get; }

        public ICommand NextCommand { get; }

        public CookingLevelViewModel()
        {
            NavigateToPreviousPageCommand = new RelayCommand(NavigateToPreviousPage);
            NavigateToNextPageCommand = new RelayCommand(NavigateToNextPage);
            NextCommand = new RelayCommand(NavigateToNextPage);
            cookingPageService = new CookingPageService();

            username = string.Empty;
            userSelectedCookingSkill = string.Empty;

            PropertyChanged = (eventSender, eventArguments) => { };
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

        public string LastName
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string SelectedCookingSkill
        {
            get => userSelectedCookingSkill;
            set
            {
                userSelectedCookingSkill = value;
                OnPropertyChanged(nameof(SelectedCookingSkill));
            }
        }

        public void SetUserInfo(string username)
        {
            Username = username;
        }

        public void NavigateToNextPage()
        {
            cookingPageService.AddCookingSkill(
                Username,
                SelectedCookingSkill);

            NavigationService.Instance.NavigateTo(typeof(DietaryPreferencesPage), this);
        }

        private void NavigateToPreviousPage()
        {
            NavigationService.Instance.GoBack();
        }

        // Override base class event to provide our own implementation
        public new event PropertyChangedEventHandler PropertyChanged;

        // Override base class method to use our own PropertyChanged event
        protected new void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
