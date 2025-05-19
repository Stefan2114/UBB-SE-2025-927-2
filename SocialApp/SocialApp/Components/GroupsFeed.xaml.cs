using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using AppCommonClasses.Enums;
using SocialApp.Repository;
using SocialApp.Services;
using System.Collections.Generic;
using System.Linq;
using AppCommonClasses.Interfaces;
using SocialApp.Proxies;
using SocialApp.ViewModels;

namespace SocialApp.Components
{
    public sealed partial class GroupsFeed : UserControl
    {
        private int currentPage = 1;
        private const int itemsPerPage = 5;
        private PostViewModel postViewModel;


        public GroupsFeed()
        {
            this.InitializeComponent();

            var userRepository = new UserServiceProxy();
            var postRepository = new PostServiceProxy();
            var groupRepository = new GroupRepository();//ar trebui sa fie Service si cu dependency injection nu cu new trebuie schimbat
            var postService = new PostServiceProxy();
            this.postViewModel = new PostViewModel(postService);
            
            LoadItems();
            DisplayCurrentPage();
        }

        private void LoadItems()
        {
            var controller = App.Services.GetService<AppController>();
            this.postViewModel.PopulatePostsGroupsFeed(controller.CurrentUser.Id);

        }

        private void DisplayCurrentPage()
        {
            GroupsStackPanel.Children.Clear();
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = startIndex + itemsPerPage;
            var allItems = this.postViewModel.GetCurrentPosts();
            for (int i = startIndex; i < endIndex && i < allItems.Count; i++)
            {
                GroupsStackPanel.Children.Add(allItems[i]);
            }
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
            var listCount = this.postViewModel.GetCurrentPosts().Count;
            if (currentPage * itemsPerPage < listCount)
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }
    }
}
