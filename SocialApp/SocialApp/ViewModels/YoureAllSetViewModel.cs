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



        public string Username
        {
            get => model.Username;
            set
            {
                model.Username = value;
                OnPropertyChanged(nameof(model.Username));
            }
        }

        public void SetUserInfo(string username)
        {
            Username = username;
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
