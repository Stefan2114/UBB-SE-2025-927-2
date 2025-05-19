using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using AppCommonClasses.Enums;
using SocialApp.Repository;
using SocialApp.Services;
using System.Collections.Generic;
using System.Linq;
using SocialApp.Proxies;
using SocialApp.ViewModels;
using SocialApp.ViewModels;
using SocialApp.Services;
using SocialApp.Interfaces;

namespace SocialApp.Components
{
    public sealed partial class PostsFeed : UserControl
    {
        private int currentPage = 1;
        private const int postsPerPage = 5;
        private PostViewModel postViewModel;

        public StackPanel PostsStackPanelPublic => PostsStackPanel;

        public PostsFeed()
        {
            this.InitializeComponent();

            var userServiceProxy = new UserServiceProxy();
            var postRepository = new PostServiceProxy();
            var groupRepository = new GroupRepository();//ar trebui sa fie Service si cu dependency injection nu cu new trebuie schimbat
            var postService = new PostServiceProxy();
            this.postViewModel = new PostViewModel(postService);


            LoadPosts();
            DisplayCurrentPage();
        }

        private void LoadPosts()
        {
            var controller = App.Services.GetService<AppController>();
            long userId;
            if (controller.CurrentUser == null)
            {
                userId = -1;
            }
            else
            {
                userId = controller.CurrentUser.Id;
            }
            this.postViewModel.PopulatePostsHomeFeed(userId);

        }


        public void DisplayCurrentPage()
        {
            PostsStackPanel.Children.Clear();
            int startIndex = (currentPage - 1) * postsPerPage;
            int endIndex = startIndex + postsPerPage;
            var allPosts = this.postViewModel.GetCurrentPosts();
            for (int i = startIndex; i < endIndex && i < allPosts.Count; i++)
            {
                PostsStackPanel.Children.Add(allPosts[i]);
            }
        }

        public void ClearPosts()
        {
            this.postViewModel.clearPosts();
        }

        public void PopulatePostsByGroupId(long groupId)
        {
            this.postViewModel.PopulatePostsByGroupId(groupId);
        }

        public void PopulatePostsByUserId(long userId)
        {
            this.postViewModel.PopulatePostsByUserId(userId);
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            var allPosts = this.postViewModel.GetCurrentPosts();
            if (currentPage * postsPerPage < allPosts.Count)
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }
    }
}
