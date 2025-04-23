namespace SocialApp.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;
    using SocialApp.Components;
    using SocialApp.Entities;
    using SocialApp.Repository;
    using SocialApp.Services;

    public sealed partial class UserPage : Page
    {
        private AppController controller;
        private IUserRepository userRepository;
        private IUserService userService;
        private IPostRepository postRepository;
        private IPostService postService;
        private IGroupRepository groupRepository;
        private User displayedUser; // User to display (may differ from CurrentUser)

        public UserPage()
        {
            this.InitializeComponent();

            this.userRepository = new UserRepository();
            this.userService = new UserService(this.userRepository);
            this.postRepository = new PostRepository();
            this.groupRepository = new GroupRepository();
            this.postService = new PostService(this.postRepository, this.userRepository, this.groupRepository);

            // Use null-coalescing operator to handle potential null reference
            this.controller = App.Services.GetService<AppController>() ?? throw new System.InvalidOperationException("AppController service is not registered.");

            this.Loaded += this.SetContent;
            this.Loaded += this.PostsClick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is UserPageNavigationArgs args)
            {
                this.displayedUser = args.SelectedUser;
                this.TopBar.SetFrame(this.Frame);
            }
            else
            {
                this.displayedUser = this.controller.CurrentUser; // Default to CurrentUser if no specific user provided
                this.TopBar.SetFrame(this.Frame);
            }
        }

        private async void SetContent(object sender, RoutedEventArgs e)
        {
            if (this.displayedUser != null)
            {
                if (!string.IsNullOrEmpty(this.displayedUser.Image))
                {
                    this.ProfileImage.Source = await AppController.DecodeBase64ToImageAsync(this.displayedUser.Image);
                }

                this.Username.Text = this.displayedUser.Username;

                if (this.controller.CurrentUser != null && this.controller.CurrentUser.Id == this.displayedUser.Id)
                {
                    this.FollowLogOutButton.Content = "Logout";
                    this.FollowLogOutButton.Click -= this.Logout;
                    this.FollowLogOutButton.Click += this.Logout;
                }
                else
                {
                    this.FollowLogOutButton.Content = this.IsFollowed() ? "Unfollow" : "Follow";
                    this.FollowLogOutButton.Click -= this.FollowUnfollow;
                    this.FollowLogOutButton.Click += this.FollowUnfollow;
                }

                this.SetPostsContent(sender, e);
            }
            else
            {
                this.FollowLogOutButton.Content = "Follow";
            }
        }

        private bool IsFollowed()
        {
            // Check if CurrentUser follows displayedUser
            return this.controller?.CurrentUser != null && this.userService.GetUserFollowing(this.controller.CurrentUser.Id).Any(u => u.Id == this.displayedUser.Id);
        }

        private void FollowUnfollow(object sender, RoutedEventArgs e)
        {
            if (this.controller?.CurrentUser != null && this.displayedUser != null)
            {
                if (this.IsFollowed())
                {
                    this.userService.UnfollowUserById(this.controller.CurrentUser.Id, this.displayedUser.Id);
                    this.FollowLogOutButton.Content = "Follow";
                }
                else
                {
                    this.userService.FollowUserById(this.controller.CurrentUser.Id, this.displayedUser.Id);
                    this.FollowLogOutButton.Content = "Unfollow";

                }
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.controller.Logout();
            this.Frame.Navigate(typeof(HomeScreen), this.controller);
        }

        private void PostsClick(object sender, RoutedEventArgs e)
        {
            this.SetPostsContent(sender, e);
            this.PostsFeed.DisplayCurrentPage();
        }

        private void SetPostsContent(object sender, RoutedEventArgs e)
        {
            this.PostsButton.IsEnabled = false;
            this.WorkoutsButton.IsEnabled = true;
            this.MealsButton.IsEnabled = true;
            this.FollowersButton.IsEnabled = true;

            this.PostsFeed.Visibility = Visibility.Visible;
            this.FollowersScrollViewer.Visibility = Visibility.Collapsed;

            this.PopulateFeed();
        }

        private void PopulateFeed()
        {
            this.PostsFeed.ClearPosts();

            if (this.displayedUser != null)
            {
                List<Post> userPosts = this.postService.GePostsByUserId(this.displayedUser.Id);
                foreach (Post post in userPosts)
                {
                    this.PostsFeed.AddPost(new PostComponent(post.Title, post.Visibility, post.UserId, post.Content, post.CreatedDate, post.Tag, post.Id));
                }

                this.PostsFeed.DisplayCurrentPage();
            }
        }

        private void WorkoutsClick(object sender, RoutedEventArgs e)
        {
            this.SetWorkoutsContent();
        }

        private void SetWorkoutsContent()
        {
            this.PostsButton.IsEnabled = true;
            this.WorkoutsButton.IsEnabled = false;
            this.MealsButton.IsEnabled = true;
            this.FollowersButton.IsEnabled = true;

            this.PostsFeed.Visibility = Visibility.Collapsed;
            this.FollowersScrollViewer.Visibility = Visibility.Collapsed;
        }

        private void MealsClick(object sender, RoutedEventArgs e)
        {
            this.SetMealsContent();
        }

        private void SetMealsContent()
        {
            this.PostsButton.IsEnabled = true;
            this.WorkoutsButton.IsEnabled = true;
            this.MealsButton.IsEnabled = false;
            this.FollowersButton.IsEnabled = true;

            this.PostsFeed.Visibility = Visibility.Collapsed;
            this.FollowersScrollViewer.Visibility = Visibility.Collapsed;
        }

        private void FollowersClick(object sender, RoutedEventArgs e)
        {
            this.SetFollowersContent();
        }

        private void SetFollowersContent()
        {
            this.PostsButton.IsEnabled = true;
            this.WorkoutsButton.IsEnabled = true;
            this.MealsButton.IsEnabled = true;
            this.FollowersButton.IsEnabled = false;

            this.PostsFeed.Visibility = Visibility.Collapsed;
            this.FollowersScrollViewer.Visibility = Visibility.Visible;

            this.PopulateFollowers();
        }

        private void PopulateFollowers()
        {
            this.FollowersStack.Children.Clear();

            if (this.displayedUser != null)
            {
                List<User> followers = this.userService.GetUserFollowers(this.displayedUser.Id);

                foreach (User user in followers)
                {
                    this.FollowersStack.Children.Add(new Follower(user.Username, this.userService.GetUserFollowing(this.controller.CurrentUser?.Id ?? -1).Contains(user), user, this.Frame));
                }
            }
        }
    }
}