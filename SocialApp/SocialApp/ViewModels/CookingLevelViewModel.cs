using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using SocialApp.Interfaces;
using SocialApp.Pages;
using SocialApp.Proxies;
using SocialApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SocialApp.ViewModels
{
    public partial class CookingLevelViewModel : ViewModelBase
    {
        private readonly ICookingPageRepository cookingPageRepository;
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
            cookingPageRepository = new CookingPageRepositoryProxy();

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
            var cookingSkill = cookingPageRepository.GetCookingSkillByDescription(SelectedCookingSkill);
            if (cookingSkill != null)
            {
                cookingPageRepository.UpdateUserCookingSkill(1, cookingSkill.Id); // TODO: Replace 1 with actual user ID
            }

            NavigationService.Instance.NavigateTo(typeof(DietaryPreferencesPage), this);
        }

        private void NavigateToPreviousPage()
        {
            NavigationService.Instance.GoBack();
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected new void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
