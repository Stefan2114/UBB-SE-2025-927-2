using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialApp.Entities;
using SocialApp.Pages;
using SocialApp.Repository;
using SocialApp.Services;
using System.Collections.Generic;

namespace SocialApp.Components
{
    public sealed partial class Follower : UserControl
    {
        private readonly User user;
        private readonly AppController controller;
        private readonly Frame navigationFrame;

        private UserRepository userRepository;
        private UserService userService;
        private PostRepository postRepository;
        private PostService postService;
        private GroupRepository groupRepository;

        public Follower(string username, bool isFollowing, User user, Frame frame = null)
        {
            this.InitializeComponent();

            userRepository = new UserRepository();
            userService = new UserService(userRepository);
            postRepository = new PostRepository();
            groupRepository = new GroupRepository();
            postService = new PostService(postRepository, userRepository, groupRepository);

            this.user = user;
            this.controller = App.Services.GetService<AppController>();
            this.navigationFrame = frame ?? Window.Current.Content as Frame; // Fallback to app-level Frame if not provided
            Name.Text = username;
            Button.Content = IsFollowed() ? "Unfollow" : "Follow";
            this.PointerPressed += Follower_Click; // Add click event to the entire control
        }

        private bool IsFollowed()
        {
            List<User> following = userService.GetUserFollowing(controller.CurrentUser.Id);
            foreach (User user in following)
            {
                if (user.Id == this.user.Id) return true;
            }

            return false;
        }

        private void Follower_Click(object sender, RoutedEventArgs e)
        {
            if (navigationFrame != null)
            {
                navigationFrame.Navigate(typeof(UserPage), new UserPageNavigationArgs(controller, user));
            }
        }

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

    // Helper class to pass both controller and user
    public class UserPageNavigationArgs
    {
        public AppController Controller { get; }
        public User SelectedUser { get; }

        public UserPageNavigationArgs(AppController controller, User selectedUser)
        {
            Controller = controller;
            SelectedUser = selectedUser;
        }
    }
}