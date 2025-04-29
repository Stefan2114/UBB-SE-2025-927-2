namespace SocialApp.ViewModels
{
    using AppCommonClasses.Models;
    using SocialApp.Pages;
    using SocialApp.Services;
    using System.ComponentModel;
    using System.Windows.Input;

    public class YoureAllSetViewModel
    {
        public ICommand NextCommand { get; }

        private readonly YoureAllSetModel model;

        public YoureAllSetViewModel()
        {
            NextCommand = new RelayCommand(GoNext);
            model = new YoureAllSetModel();
        }

        public string FirstName
        {
            get => model.FirstName;
            set
            {
                model.FirstName = value;
                OnPropertyChanged(nameof(model.FirstName));
            }
        }

        public string LastName
        {
            get => model.LastName;
            set
            {
                model.LastName = value;
                OnPropertyChanged(nameof(model.LastName));
            }
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GoNext()
        {
            NavigationService.Instance.NavigateTo(typeof(MainPage), this);
        }
    }
}
