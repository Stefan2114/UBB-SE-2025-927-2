namespace SocialApp.ViewModels
{
    using AppCommonClasses.Models;
    using SocialApp.Interfaces;
    using SocialApp.Pages;
    using SocialApp.Services;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public class DietaryPreferencesViewModel : INotifyPropertyChanged
    {
        public ICommand BackCommand { get; set; }

        public ICommand NextCommand { get; set; }

        private readonly IDietaryPreferencesService dietaryPreferencesService;

        public ObservableCollection<string> OtherDietOptions { get; set; }

        public ObservableCollection<string> AllergenOptions { get; set; }

        private string otherDiet = DietaryPreferenceType.NotSelected.ToString();

        public string OtherDiet
        {
            get => otherDiet;
            set
            {
                if (otherDiet != value)
                {
                    otherDiet = value;
                    OnPropertyChanged(nameof(OtherDiet));
                }
            }
        }

        private string allergens = DietaryPreferenceType.NotSelected.ToString();

        public string Allergens
        {
            get => allergens;
            set
            {
                if (allergens != value)
                {
                    allergens = value;
                    OnPropertyChanged(nameof(Allergens));
                }
            }
        }

        public DietaryPreferencesViewModel()
        {
            dietaryPreferencesService = new DietaryPreferencesService();
            BackCommand = new RelayCommand(BackAction);
            NextCommand = new RelayCommand(NextAction);

            // Populate options
            OtherDietOptions = new ObservableCollection<string>
        {
            "None", "Mediterranean", "Low-Fat", "Diabetic-Friendly", "Kosher", "Halal",
        };

            AllergenOptions = new ObservableCollection<string>
        {
            "None", "Peanuts", "Tree Nuts", "Dairy", "Eggs", "Gluten", "Shellfish", "Soy", "Fish", "Sesame",
        };
        }

        private void BackAction()
        {
            NavigationService.Instance.GoBack();
        }

        public void NextAction()
        {
            dietaryPreferencesService.AddAllergyAndDietaryPreference(Username, OtherDiet, Allergens);
            NavigationService.Instance.NavigateTo(typeof(YoureAllSetPage), this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string username;

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
            Username = username;
        }
    }
}
