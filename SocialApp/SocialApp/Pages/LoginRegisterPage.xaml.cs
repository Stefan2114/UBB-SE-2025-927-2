using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace SocialApp.Pages
{


    public sealed partial class LoginRegisterPage : Page
    {
        private const Visibility CollapsedVisibility = Visibility.Collapsed;
        private const Visibility VisibleVisibility = Visibility.Visible;
        private AppController appController;
        private string profileImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginRegisterPage"/> class.
        /// </summary>
        public LoginRegisterPage()
        {
            InitializeComponent();
            this.InitializePageFlow();

        }

        /// <summary>
        /// Handles the click event of the continue button.
        /// </summary>
        /// <param name="sender">
        /// the object that raised the event.
        /// </param>
        /// <param name="e">
        /// the event data.
        /// </param>
        //public void ContinueClick(object sender, RoutedEventArgs e)
        //{
        //    if (this.controller.EmailExists(this.EmailTextbox.Text))
        //    {
        //        this.LoginFlow();
        //    }
        //    else
        //    {
        //        this.RegisterFlow();
        //    }
        //}

        /// <summary>
        /// Handles the navigation to this page.
        /// </summary>
        /// <param name="e">
        /// the event data that contains the navigation parameters.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// thrown when the AppController service is not found.
        /// </exception>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.appController = App.Services.GetService<AppController>();

        }

        private void InitializePageFlow()
        {
            this.SetInitialVisibilityStates();
            this.SetInitialContentValues();
            this.SetInitialEventHandlers();

        }

        private void SetInitialVisibilityStates()
        {
            EmailTextbox.Visibility = VisibleVisibility;
            UsernameTextbox.Visibility = CollapsedVisibility;
            PasswordTextbox.Visibility = CollapsedVisibility;
            ConfirmPasswordTextbox.Visibility = CollapsedVisibility;
            UploadedImage.Visibility = CollapsedVisibility;
            UploadImageButton.Visibility = CollapsedVisibility;
            RemoveImageButton.Visibility = CollapsedVisibility;
            CheckBox.Visibility = CollapsedVisibility;
            ContinueButton.Visibility = VisibleVisibility;

        }

        private void SetInitialContentValues()
        {
            this.PageName.Text = "Login/Register";
            this.ContinueButton.Content = "Continue";
        }

        private void SetInitialEventHandlers()
        {
            ContinueButton.Click += OnContinueClicked;
        }

        public void OnContinueClicked(object sender, RoutedEventArgs e)
        {
            if (this.appController.EmailExists(EmailTextbox.Text))
            {
                this.InitializeLoginFlow();
            }
            else
            {
                this.InitializeRegistrationFlow();
            }

        }

        private void InitializeLoginFlow()
        {
            this.SetLoginVisibilityStates();
            this.SetLoginContentValues();
            this.SetLoginEventHandlers();

        }

        private void SetLoginVisibilityStates()
        {
            this.PasswordTextbox.Visibility = VisibleVisibility;

        }

        private void SetLoginContentValues()
        {

            this.PageName.Text = "Login";
            this.ContinueButton.Content = "Login";
            this.ErrorTextbox.Text = string.Empty;
        }

        private void SetLoginEventHandlers()
        {
            ContinueButton.Click -= OnContinueClicked;
            ContinueButton.Click += OnLoginClicked;

        }

        private void OnLoginClicked(object sender, RoutedEventArgs e)
        {

            if (!this.appController.Login(this.EmailTextbox.Text, this.PasswordTextbox.Password)) // Use Password property
            {
                this.ErrorTextbox.Visibility = VisibleVisibility;
                this.ErrorTextbox.Text = "Incorrect password.";
                this.PasswordTextbox.Password = string.Empty;
            }
            else
            {
                this.Frame.Navigate(typeof(HomeScreen), this.appController);
            }
        }

        private void InitializeRegistrationFlow()
        {
            this.SetRegistrationVisibilityStates();
            this.SetRegistrationContentValues();
            this.SetRegistrationEventHandlers();

        }

        private void SetRegistrationVisibilityStates()
        {
            PasswordTextbox.Visibility = VisibleVisibility;
            UsernameTextbox.Visibility = VisibleVisibility;
            ConfirmPasswordTextbox.Visibility = VisibleVisibility;
            UploadedImage.Visibility = VisibleVisibility;
            UploadImageButton.Visibility = VisibleVisibility;
            RemoveImageButton.Visibility = VisibleVisibility;
            CheckBox.Visibility = VisibleVisibility;

        }

        private void SetRegistrationContentValues()
        {

            this.PageName.Text = "Register";
            this.ContinueButton.Content = "Register";
            this.ErrorTextbox.Text = string.Empty;
            this.UploadedImage.Child = new Image
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/User.png")),
            };
            this.profileImage = string.Empty;

        }

        private void SetRegistrationEventHandlers()
        {
            ContinueButton.Click -= OnContinueClicked;
            ContinueButton.Click += OnRegisterClicked;
        }

        private async void OnImageUpload(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePicker = new FileOpenPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                };
                filePicker.FileTypeFilter.Add(".jpg");
                filePicker.FileTypeFilter.Add(".jpeg");
                filePicker.FileTypeFilter.Add(".png");

                var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(App.CurrentWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(filePicker, windowHandle);

                StorageFile selectedFile = await filePicker.PickSingleFileAsync();
                if (selectedFile != null)
                {
                    this.profileImage = await AppController.EncodeImageToBase64Async(selectedFile);

                    var bitmapImage = new BitmapImage();
                    using (IRandomAccessStream fileStream = await selectedFile.OpenAsync(FileAccessMode.Read))
                    {
                        await bitmapImage.SetSourceAsync(fileStream);
                    }

                    this.UploadedImage.Child = new Image { Source = bitmapImage };
                }
            }
            catch (Exception exception)
            {
                ErrorTextbox.Text = $"Error uploading image: {exception.Message}";

            }
        }

        private void OnImageRemoval(object sender, RoutedEventArgs e)
        {
            this.profileImage = string.Empty;
            UploadedImage.Child = new Image

            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/User.png")),
            };
        }

        private void OnRegisterClicked(object sender, RoutedEventArgs e)
        {
            try
            {

                ValidatePasswordMatch(this.PasswordTextbox.Password, this.ConfirmPasswordTextbox.Password); // Use Password property
                ValidateTermsAcceptance();
                CompleteRegistration();
            }
            catch (Exception exception)
            {
                ErrorTextbox.Text = exception.Message;

            }
        }

        private static void ValidatePasswordMatch(string password, string confirmedPassword)
        {
            if (password != confirmedPassword)
            {
                throw new Exception("Passwords must match");
            }
        }

        private void ValidateTermsAcceptance()
        {
            if (this.CheckBox.IsChecked == null || this.CheckBox.IsChecked == false)
            {
                throw new Exception("You must accept the terms and conditions!");
            }
        }

        private void CompleteRegistration()
        {
            try
            {

                this.appController.Register(this.UsernameTextbox.Text, this.EmailTextbox.Text, this.PasswordTextbox.Password, this.profileImage); // Use Password property
                this.Frame.Navigate(typeof(HomeScreen), this.appController);
            }
            catch (Exception exception)
            {
                ErrorTextbox.Text = exception.Message;

            }
        }
    }
}