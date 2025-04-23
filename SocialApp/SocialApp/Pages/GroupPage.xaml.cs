using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialApp.Repository;
using SocialApp.Services;
using SocialApp.Components;
using SocialApp.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace SocialApp.Pages
{
    public sealed partial class GroupPage : Page
    {
        private const Visibility collapsed = Visibility.Collapsed;
        private const Visibility visible = Visibility.Visible;
        private IUserRepository userRepository;
        private IUserService userService;
        private IPostRepository postRepository;
        private IPostService postService;
        private IGroupRepository groupRepository;
        private IGroupService groupService;
        private long GroupId;
        private Entities.Group group;

        public GroupPage()
        {
            this.InitializeComponent();
            this.Loaded += DisplayPage;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is long id)
            {
                GroupId = id;
            }
            TopBar.SetFrame(this.Frame);
            TopBar.SetNone();
        }

        private void DisplayPage(object sender, RoutedEventArgs e)
        {
            userRepository = new UserRepository();
            userService = new UserService(userRepository);
            groupRepository = new GroupRepository();
            groupService = new GroupService(groupRepository, userRepository);
            postRepository = new PostRepository();
            postService = new PostService(postRepository, userRepository, groupRepository);
            group = groupService.GetGroupById(GroupId);

            SetVisibilities();
            SetContent();
            PopulateMembers();
        }

        private void SetVisibilities()
        {
            bool isAdmin = UserIsAdmin();
        }

        private bool UserIsAdmin()
        {
            var controller = App.Services.GetService<AppController>();
            if (controller.CurrentUser == null) return false;
            return groupRepository.GetGroupById(GroupId).AdminId == controller.CurrentUser.Id;
        }

        private async void SetContent()
        {
            GroupTitle.Text = group.Name;
            GroupDescription.Text = group.Description;
            if (!string.IsNullOrEmpty(group.Image))
                GroupImage.Source = await AppController.DecodeBase64ToImageAsync(group.Image);
            PopulateFeed();
        }

        private void PopulateFeed()
        {
            PostsFeed.ClearPosts();
            List<Post> groupPosts = postService.GetPostsByGroupId(GroupId);
            foreach (Post post in groupPosts)
            {
                PostsFeed.AddPost(new PostComponent(post.Title, post.Visibility, post.UserId, post.Content, post.CreatedDate, post.Tag, post.Id));
            }
            PostsFeed.Visibility = Visibility.Visible;
            PostsFeed.DisplayCurrentPage();
        }

        private void PopulateMembers()
        {
            MembersList.Children.Clear();
            bool isAdmin = UserIsAdmin();
            List<User> members = groupService.GetUsersFromGroup(GroupId);
            foreach (User member in members)
            {
                MembersList.Children.Add(new Member(member, this.Frame, GroupId, isAdmin));
            }
        }

        private void CreatePostInGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreatePost));
        }
    }
}