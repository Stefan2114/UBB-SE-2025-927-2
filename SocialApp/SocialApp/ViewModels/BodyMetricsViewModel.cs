namespace SocialApp.ViewModels
{
    using SocialApp.Interfaces;
    using SocialApp.Pages;
    using SocialApp.Queries;
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
        private string firstName = string.Empty;
        private string lastName = string.Empty;

        public BodyMetricsViewModel()
            : this(new BodyMetricService())
        {
            // Initialize _bodyMetricService with the IDataLink dependency
            bodyMetricService = new BodyMetricService(DataLink.Instance);
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

        public BodyMetricsViewModel(IBodyMetricService bodyMetricService)
        {
            this.bodyMetricService = bodyMetricService ?? throw new ArgumentNullException(nameof(bodyMetricService));
            SubmitBodyMetricsCommand = new RelayCommand(GoNext);
        }

        public void SetUserInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void GoNext()
        {
            try
            {
                bodyMetricService.UpdateUserBodyMetrics(FirstName, LastName, Weight, Height, TargetWeight);
                App.NavigationController.NavigateTo(typeof(GoalPage), this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GoNext: {ex.Message}");
            }
        }
    }
}
