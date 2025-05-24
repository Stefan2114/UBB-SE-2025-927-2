namespace SocialApp.ViewModels
{
    using AppCommonClasses.Models;
    using SocialApp.Pages;
    using SocialApp.Services;
    using System.ComponentModel;
    using System.Windows.Input;

    public class YoureAllSetViewModel : INotifyPropertyChanged
    {
        public ICommand NextCommand { get; }

        private string username;

        public YoureAllSetViewModel()
        {
            NextCommand = new RelayCommand(GoNext);
            username = string.Empty; // Initialize with an empty string  
        }

        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged(nameof(Username));
                }
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
