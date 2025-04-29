using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using SocialApp.Repository;
using AppCommonClasses.Models;
using SocialApp.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Net.Mime.MediaTypeNames;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Storage;
using SocialApp.Interfaces;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateGroup : Page
    {
        private AppController controller;
        private IGroupService groupService;
        private IUserService userService;
        private string image = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGroup"/> class.
        /// </summary>
        public CreateGroup()
        {
            InitializeComponent();
            InitializeServices();
            GroupNameInput.TextChanged += GroupNameInput_TextChanged;
        }

        /// <summary>
        /// Initializes the services required for group and user management.
        /// </summary>
        private void InitializeServices()
        {
            var groupRepository = new GroupRepository();
            var userRepository = new UserRepository();
            groupService = new GroupService(groupRepository, userRepository);
            userService = new UserService(userRepository);
            controller = App.Services.GetService<AppController>();
        }

        /// <summary>
        /// This method is called when the page is navigated to. It sets the frame for the top bar.
        /// </summary>
        /// <param name="e">The event data that provides information about the navigation.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TopBar.SetFrame(Frame);
        }

        /// <summary>
        /// Handles the TextChanged event for the GroupNameInput field.
        /// Updates the character counter to reflect the current length of the group name input.
        /// </summary>
        /// <param name="sender">The source of the event, typically the GroupNameInput control.</param>
        /// <param name="e">The event data that provides information about the TextChanged event.</param>
        private void GroupNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            GroupNameCharCounter.Text = $"{GroupNameInput.Text.Length}/55";
        }


        /// <summary>
        /// Handles the TextChanged event for the GroupDescriptionInput field.
        /// Updates the character counter to reflect the current length of the group description input.
        /// </summary>
        /// <param name="sender">The source of the event, typically the GroupDescriptionInput control.</param>
        /// <param name="e">The event data that provides information about the TextChanged event.</param>
        private void GroupDescriptionInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            GroupDescriptionCharCounter.Text = $"{GroupDescriptionInput.Text.Length}/250";
        }

        /// <summary>
        /// Handles the click event for the Cancel button.
        /// Navigates back to the previous page in the frame.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Cancel button control.</param>
        /// <param name="e">The event data that provides information about the click event.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        /// <summary>
        /// Handles the click event for the Create Group button.
        /// Validates the input fields and creates a new group if valid.
        /// Navigates to the UserPage upon successful creation.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Create Group button control.</param>
        /// <param name="e">The event data that provides information about the click event.</param>
        private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateInputs();

                var newGroup = new AppCommonClasses.Models.Group
                {
                    Name = GroupNameInput.Text.Trim(),
                    Description = string.IsNullOrWhiteSpace(GroupDescriptionInput.Text) ? null : GroupDescriptionInput.Text.Trim(),
                    AdminId = this.controller.CurrentUser.Id,
                    Image = image
                };

                groupService.AddGroup(newGroup.Name, newGroup.Description ?? "", newGroup.Image ?? "", newGroup.AdminId);
                Frame.Navigate(typeof(UserPage), controller);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Validates the input fields for creating a new group.
        /// Throws an exception if any validation rule is violated.
        /// </summary>
        /// <exception cref="Exception">Thrown when the group name is empty, exceeds 55 characters, or when the group description exceeds 250 characters.</exception>
        private void ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(GroupNameInput.Text))
                throw new Exception("Group name is required!");

            if (GroupNameInput.Text.Length > 55)
                throw new Exception("Group name cannot exceed 55 characters!");

            if (GroupDescriptionInput.Text.Length > 250)
                throw new Exception("Group description cannot exceed 250 characters!");
        }

        /// <summary>
        /// Displays an error message to the user.
        /// This method sets the text of the ErrorMessage control to the provided message
        /// and makes the ErrorMessage control visible. It is typically used to inform
        /// the user of validation errors or other issues that need attention.
        /// </summary>
        /// <param name="message">The error message to be displayed.</param>
        private void ShowError(string message)
        {
            ErrorMessage.Text = message;
            ErrorMessage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the TextChanged event for the UserSearchBox field.
        /// Filters the list of users based on the current input in the search box.
        /// If the input is empty, it hides the search results.
        /// Otherwise, it displays the users that match the search query.
        /// </summary>
        /// <param name="sender">The source of the event, typically the UserSearchBox control.</param>
        /// <param name="e">The event data that provides information about the TextChanged event.</param>
        private void UserSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = UserSearchBox.Text;

            if (string.IsNullOrWhiteSpace(query))
            {
                // Hide search results but don't affect layout
                UserSearchResults.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Show filtered results
                var users = userService.GetUserFollowing(controller.CurrentUser.Id)
                                        .Where(u => u.Username.Contains(query))
                                        .ToList();
                UserSearchResults.ItemsSource = users;
                UserSearchResults.Visibility = Visibility.Visible;
            }

            // Manually update layout (if needed)
            RootGrid.UpdateLayout();
        }



        /// <summary>
        /// Handles the selection change event for the UserSearchResults list.
        /// This method is triggered when the user selects a user from the search results.
        /// It adds the selected user to the selected users list for the group creation process.
        /// </summary>
        /// <param name="sender">The source of the event, typically the UserSearchResults control.</param>
        /// <param name="e">The event data that provides information about the selection change.</param>
        private void UserSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserSearchResults.SelectedItem is User selectedUser)
            {
                AddUserToSelectedList(selectedUser);
            }
        }

        /// <summary>
        /// Adds a user to the selected users list for group creation.
        /// This method creates a button representing the selected user,
        /// which can be clicked to remove the user from the selection.
        /// </summary>
        /// <param name="user">The user to be added to the selected users list.</param>
        private void AddUserToSelectedList(User user)
        {
            // Create small version with an "X"
            var smallUserButton = new Button()
            {
                Content = $"{user.Username} X",
                Tag = user,
                Background = new SolidColorBrush(Microsoft.UI.Colors.Cyan),
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
            };
            smallUserButton.Click += (s, e) => RemoveUserFromSelectedList(user);

            // Add to the selected users panel
            SelectedUsersPanel.Children.Add(smallUserButton);
        }

        /// <summary>
        /// Handles the Tapped event for the UserSearchBox control.
        /// This method is triggered when the user taps inside the search box.
        /// It makes the search results visible, allowing the user to see potential matches
        /// for their input. This enhances the user experience by providing immediate feedback
        /// on available users to add to the group.
        /// </summary>
        /// <param name="sender">The source of the event, typically the UserSearchBox control.</param>
        /// <param name="e">The event data that provides information about the Tapped event.</param>
        /// Note: This method does not seem to be necessary in the current context,
        private void UserSearchBox_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // Show the search results when the user taps inside the search box.
            UserSearchResults.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the Tapped event for the UserSearchResults control.
        /// This method is triggered when the user taps on a user from the search results.
        /// It adds the selected user to the selected users list for the group creation process.
        /// Optionally, it can hide the search results after selection.
        /// </summary>
        /// <param name="sender">The source of the event, typically the UserSearchResults control.</param>
        /// <param name="e">The event data that provides information about the Tapped event.</param>
        /// Note: This method does not seem to be necessary in the current context,
        private void UserSearchResults_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            // Allow users to select a user from the list, without hiding the dropdown immediately.
            var selectedUser = (User)((ListBox)sender).SelectedItem;
            AddUserToSelectedList(selectedUser);
            // Optionally, you can hide the results after selection
            UserSearchResults.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Removes a user from the selected users list for group creation.
        /// This method searches for the button representing the specified user
        /// in the SelectedUsersPanel and removes it if found. This allows the user
        /// to deselect a user that was previously added to the group.
        /// </summary>
        /// <param name="user">The user to be removed from the selected users list.</param>
        private void RemoveUserFromSelectedList(User user)
        {
            var smallUserButton = SelectedUsersPanel.Children
                .OfType<Button>()
                .FirstOrDefault(button => button.Tag == user);

            if (smallUserButton != null)
            {
                SelectedUsersPanel.Children.Remove(smallUserButton);
            }
        }

        /// <summary>
        /// Handles the click event for the Add Image button.
        /// This method opens a file picker dialog to allow the user to select an image file.
        /// If a file is selected, it encodes the image to a Base64 string and stores it in the 'image' variable.
        /// If an error occurs during the process, it displays an error message in the ErrorTextBox.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Add Image button control.</param>
        /// <param name="e">The event data that provides information about the click event.</param>
        private async void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var picker = new FileOpenPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary
                };
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".jpeg");
                picker.FileTypeFilter.Add(".png");

                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.CurrentWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

                StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    image = await AppController.EncodeImageToBase64Async(file);
                }
            }
            catch (Exception ex)
            {
                ErrorTextBox.Text = $"Error uploading image: {ex.Message}";
            }
        }

        /// <summary>
        /// Handles the click event for the Remove Image button.
        /// This method clears the currently selected image by setting the 'image' variable to an empty string.
        /// It allows the user to remove an image that was previously selected for the group.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Remove Image button control.</param>
        /// <param name="e">The event data that provides information about the click event.</param>
        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            image = string.Empty;
        }
    }
}
