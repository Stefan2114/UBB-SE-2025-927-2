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

        private string userFirstName;
        private string userLastName;
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

            userFirstName = string.Empty;
            userLastName = string.Empty;
            userSelectedCookingSkill = string.Empty;

            PropertyChanged = (eventSender, eventArguments) => { };
        }

        public string FirstName
        {
            get => userFirstName;
            set
            {
                userFirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => userLastName;
            set
            {
                userLastName = value;
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

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void NavigateToNextPage()
        {
            cookingPageService.AddCookingSkill(
                FirstName,
                LastName,
                SelectedCookingSkill);

            App.NavigationController.NavigateTo(typeof(DietaryPreferencesPage), this);
        }

        private void NavigateToPreviousPage()
        {
            App.NavigationController.GoBack();
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
