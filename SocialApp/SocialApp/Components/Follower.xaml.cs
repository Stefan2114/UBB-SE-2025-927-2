namespace SocialApp.Components
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml;
    using AppCommonClasses.Models;
    using SocialApp.Pages;
    using SocialApp.Repository;
    using SocialApp.Services;
    using System.Collections.Generic;
    using SocialApp.Proxies;
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;

    /// <summary>
    /// Represents a user control for displaying a follower in the social app.
    /// </summary>
    public sealed partial class Follower : UserControl
    {
        private readonly User user;

        private readonly AppController controller;

        private readonly Frame navigationFrame;

        private IUserRepository userRepository;

        private IUserService userService;

        private IPostRepository postRepository;

        private IPostService postService;

        private IGroupRepository groupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Follower"/> class.
        /// </summary>
        /// <param name="username">The username of the follower.</param>
        /// <param name="isFollowing">Indicates whether the current user is following this user.</param>
        /// <param name="user">The user object associated with the follower.</param>
        /// <param name="frame">The navigation frame for navigating to user pages.</param>
        public Follower(string username, bool isFollowing, User user, Frame frame = null)
        {
            this.InitializeComponent();

            userRepository = new UserRepository();
            userService = new UserService(userRepository);
            postRepository = new PostRepositoryProxy();
            groupRepository = new GroupRepository();
            postService = new PostService(postRepository, userRepository, groupRepository);

            this.user = user;
            this.controller = App.Services.GetService<AppController>();
            this.navigationFrame = frame ?? Window.Current.Content as Frame; // Fallback to app-level Frame if not provided
            Name.Text = username;
            Button.Content = IsFollowed() ? "Unfollow" : "Follow";
            this.PointerPressed += Follower_Click; // Add click event to the entire control
        }

        /// <summary>
        /// Determines whether the current user is following this user.
        /// </summary>
        /// <returns>True if the user is followed; otherwise, false.</returns>
        private bool IsFollowed()
        {
            List<User> following = userService.GetUserFollowing(controller.CurrentUser.Id);
            foreach (User user in following)
            {
                if (user.Id == this.user.Id) return true;
            }

            return false;
        }

        /// <summary>
        /// Handles the click event for the follower control.
        /// </summary>
        private void Follower_Click(object sender, RoutedEventArgs e)
        {
            if (navigationFrame != null)
            {
                navigationFrame.Navigate(typeof(UserPage), new UserPageNavigationArgs(controller, user));
            }
        }

        /// <summary>
        /// Handles the click event for the follow/unfollow button.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button.Content = Button.Content.ToString() == "Follow" ? "Unfollow" : "Follow";
            if (!IsFollowed())
            {
                userService.FollowUserById(controller.CurrentUser.Id, user.Id);
            }
            else
            {
                userService.UnfollowUserById(controller.CurrentUser.Id, user.Id);
            }
        }
    }

    /// <summary>
    /// Helper class to pass both controller and user for navigation.
    /// </summary>
    public class UserPageNavigationArgs
    {
        /// <summary>
        /// Gets the application controller.
        /// </summary>
        public AppController Controller { get; }

        /// <summary>
        /// Gets the selected user.
        /// </summary>
        public User SelectedUser { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPageNavigationArgs"/> class.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        /// <param name="selectedUser">The selected user.</param>
        public UserPageNavigationArgs(AppController controller, User selectedUser)
        {
            Controller = controller;
            SelectedUser = selectedUser;
        }
    }
}