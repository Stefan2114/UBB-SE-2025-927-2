namespace SocialApp.ViewModels
{
    using AppCommonClasses.Interfaces;
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
            // Create the service proxy directly
            bodyMetricService = new BodyMetricServiceProxy();
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
                // First, validate inputs
                if (string.IsNullOrWhiteSpace(Username))
                {
                    Debug.WriteLine("Error: Username is required");
                    return;
                }

                if (!float.TryParse(Weight, out float weightValue))
                {
                    Debug.WriteLine("Error: Invalid weight format");
                    return;
                }

                if (!float.TryParse(Height, out float heightValue))
                {
                    Debug.WriteLine("Error: Invalid height format");
                    return;
                }

                // Parse target weight (can be null)
                float? targetWeightValue = null;
                if (!string.IsNullOrWhiteSpace(TargetWeight))
                {
                    if (float.TryParse(TargetWeight, out float parsedTarget))
                    {
                        targetWeightValue = parsedTarget;
                    }
                    else
                    {
                        Debug.WriteLine("Error: Invalid target weight format");
                        return;
                    }
                }

                // Now call the service with properly converted values
                bodyMetricService.UpdateUserBodyMetrics(
                    Username,
                    weightValue,
                    heightValue,
                    targetWeightValue);

                NavigationService.Instance.NavigateTo(typeof(GoalPage), this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GoNext: {ex.Message}");
            }
        }


    }
}
