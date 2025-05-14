using AppCommonClasses.Enums;
using AppCommonClasses.Models;
using AppCommonClasses.Repos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialApp.Interfaces;
using SocialApp.Proxies;
using SocialApp.Repository;
using SocialApp.Services;
using SocialApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;
using Group = AppCommonClasses.Models.Group;

namespace SocialApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreatePost : Page
    {
        private AppController controller;
        private PostViewModel postViewModel;
        private IGroupService groupService;
        private List<Group> userGroups = new List<Group>();
        private string image = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePost"/> class.
        /// </summary>
        public CreatePost()
        {
            this.InitializeComponent();
            this.InitializeServices();
            this.TitleInput.TextChanged += this.TitleInput_TextChanged;
            this.DescriptionInput.TextChanged += this.DescriptionInput_TextChanged;
        }

        /// <inheritdoc/>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.TopBar.SetFrame(this.Frame);
            this.TopBar.SetCreate();
            this.LoadUserGroups();
            this.InitializeVisibilityOptions();
        }

        private void InitializeServices()
        {
            this.controller = App.Services.GetService<AppController>();
            var postService = App.Services.GetService<PostService>();
            this.postViewModel = new PostViewModel(postService);
            //this.groupService = App.Services.GetService<GroupService>();
            // nu exista GroupRepoProxy, va trebui facut direct un GroupServiceProxxy
            this.groupService = new GroupService(new Repository.GroupRepository(), new UserRepositoryProxy());
        }

        private void LoadUserGroups()
        {
            if (this.controller?.CurrentUser == null)
            {
                throw new InvalidOperationException("CurrentUser is not set in the AppController.");
            }

            this.userGroups = this.groupService.GetGroups(this.controller.CurrentUser.Id);
            this.GroupsListBox.ItemsSource = this.userGroups;
        }

        private void InitializeVisibilityOptions()
        {
            this.VisibilityComboBox.ItemsSource = Enum.GetValues(typeof(PostVisibility));
            this.VisibilityComboBox.SelectedIndex = 0;
        }

        private void TitleInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TitleCharCounter.Text = $"{this.TitleInput.Text.Length}/50";
        }

        private void DescriptionInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.DescriptionCharCounter.Text = $"{this.DescriptionInput.Text.Length}/250";
        }

        private void VisibilityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.GroupSelectionPanel.Visibility = ((PostVisibility)this.VisibilityComboBox.SelectedItem) == PostVisibility.Groups
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private async void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var picker = new FileOpenPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                };
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".jpeg");
                picker.FileTypeFilter.Add(".png");

                var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.CurrentWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

                StorageFile file = await picker.PickSingleFileAsync();
                if (file != null)
                {
                    this.image = "image://" + await AppController.EncodeImageToBase64Async(file);
                    this.DescriptionInput.Text = "";
                    this.Confirmation.Text = "Image uploaded.";
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage.Text = $"Error uploading image: {ex.Message}";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ValidateInputs();
                var selectedVisibility = (PostVisibility)this.VisibilityComboBox.SelectedItem;
                var posts = this.CreatePosts(selectedVisibility);
                for (int i = 0; i < posts.Count; i++)
                {
                    // Use null-coalescing operator to provide a default value for GroupId
                    this.postViewModel.AddPost(
                        posts[i].Title,
                        posts[i].Content,
                        posts[i].UserId,
                        posts[i].GroupId, // Fix: Convert nullable long to long
                        posts[i].Visibility,
                        posts[i].Tag
                    );
                }

                this.Frame.Navigate(typeof(HomeScreen));
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(this.TitleInput.Text))
            {
                throw new Exception("Title is required!");
            }

            if (this.image == string.Empty && string.IsNullOrWhiteSpace(this.DescriptionInput.Text))
            {
                throw new Exception("Content cannot be empty!");
            }

            var selectedVisibility = (PostVisibility)this.VisibilityComboBox.SelectedItem;
            if (selectedVisibility == PostVisibility.Groups &&
               !this.GroupsListBox.SelectedItems.Any())
            {
                throw new Exception("Please select at least one group!");
            }
        }

        private List<Post> CreatePosts(PostVisibility visibility)
        {
            if (this.controller?.CurrentUser == null)
            {
                throw new InvalidOperationException("CurrentUser is not set in the AppController.");
            }

            var posts = new List<Post>();
            var basePost = new Post
            {
                Title = this.TitleInput.Text.Trim(),
                Content = this.DescriptionInput.Text.Trim(),
                UserId = this.controller.CurrentUser.Id,
                GroupId = 0,
                CreatedDate = DateTime.Now,
                Visibility = visibility,
                Tag = this.GetSelectedTag(),
            };

            if (visibility == PostVisibility.Groups)
            {
                Debug.WriteLine(this.GroupsListBox.Items);

                if (this.GroupsListBox.SelectedItems.Count == 0)
                {
                    throw new Exception("Please select at least one group!");
                }

                foreach (Group group in this.GroupsListBox.SelectedItems)
                {
                    posts.Add(new Post
                    {
                        Title = basePost.Title,
                        Content = basePost.Content,
                        UserId = basePost.UserId,
                        GroupId = group.Id,
                        CreatedDate = basePost.CreatedDate,
                        Visibility = PostVisibility.Groups,
                        Tag = basePost.Tag,
                    });
                }
            }
            else
            {
                posts.Add(basePost);
            }

            return posts;
        }

        private PostTag GetSelectedTag()
        {
            if (this.WorkoutRadioButton.IsChecked == true)
            {
                return PostTag.Workout;
            }

            if (this.FoodRadioButton.IsChecked == true)
            {
                return PostTag.Food;
            }

            return PostTag.Misc;
        }

        private void ShowError(string message)
        {
            this.ErrorMessage.Text = message;
            this.ErrorMessage.Visibility = Visibility.Visible;
        }
    }
}
