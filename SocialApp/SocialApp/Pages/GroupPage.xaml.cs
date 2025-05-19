using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SocialApp.Repository;
using SocialApp.Services;
using SocialApp.Components;
using AppCommonClasses.Models;
using Microsoft.Extensions.DependencyInjection;
using SocialApp.Proxies;
using AppCommonClasses.Interfaces;
using SocialApp.Interfaces;
using Server.Proxies;

namespace SocialApp.Pages
{
    public sealed partial class GroupPage : Page
    {
        private const Visibility collapsed = Visibility.Collapsed;
        private const Visibility visible = Visibility.Visible;
        private IUserService userService;
        private IPostService postService;
        private IGroupRepository groupRepository;
        private IGroupService groupService;
        private long GroupId;
        private AppCommonClasses.Models.Group group;

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
            userService = new UserServiceProxy();
            groupRepository = new GroupRepository();
            groupService = new GroupServiceProxy();
            postService = new PostServiceProxy();
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
            this.PostsFeed.ClearPosts();
            this.PostsFeed.PopulatePostsByGroupId(GroupId);
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