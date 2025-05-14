namespace SocialApp.ViewModels
{
    using SocialApp.Interfaces;
    using SocialApp.Pages;
    using SocialApp.Proxies;
    using SocialApp.Queries;
    using SocialApp.Repository;
    using SocialApp.Services;
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public class BodyMetricsViewModel : ViewModelBase
    {
        private readonly IBodyMetricService bodyMetricService;

        public ICommand SubmitBodyMetricsCommand { get; }

        private string weight = string.Empty;
        private string height = string.Empty;
        private string targetWeight = string.Empty;
        private string username = string.Empty;


        public BodyMetricsViewModel()
        {
            // Create both required dependencies
            var bodyMetricRepository = new BodyMetricRepositoryProxy();
            var userService = new UserServiceProxy();

            // Pass both dependencies to BodyMetricService
            bodyMetricService = new BodyMetricService(bodyMetricRepository, userService);
            SubmitBodyMetricsCommand = new RelayCommand(GoNext);
        }

        public string Weight
        {
            get => weight;
            set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        public string Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public string TargetWeight
        {
            get => targetWeight;
            set
            {
                targetWeight = value;
                OnPropertyChanged(nameof(TargetWeight));
            }
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


        public BodyMetricsViewModel(IBodyMetricService bodyMetricService)
        {
            this.bodyMetricService = bodyMetricService ?? throw new ArgumentNullException(nameof(bodyMetricService));
            SubmitBodyMetricsCommand = new RelayCommand(GoNext);
        }

        public void SetUserInfo(string username)
        {
            Username = username;
        }

        public void GoNext()
        {
            try
            {
                bodyMetricService.UpdateUserBodyMetrics(Username, Weight, Height, TargetWeight);
                NavigationService.Instance.NavigateTo(typeof(GoalPage), this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GoNext: {ex.Message}");
            }
        }
    }
}
