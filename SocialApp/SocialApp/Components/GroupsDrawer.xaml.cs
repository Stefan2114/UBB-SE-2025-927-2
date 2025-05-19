using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System.Collections.Generic;
using AppCommonClasses.Models;
using AppCommonClasses.Interfaces;
using SocialApp.Services;
using SocialApp.Pages;
using Windows.Networking.Sockets;
using Microsoft.Extensions.DependencyInjection;
using System;
using SocialApp.Proxies;
using AppCommonClasses.Repos; // For GroupPage

namespace SocialApp.Components
{
    public sealed partial class GroupsDrawer : UserControl
    {
        private IGroupService groupService;
        private Frame navigationFrame; // Frame for navigation
        private AppController controller;

        // Public property to set the Frame from the parent page
        public Frame NavigationFrame
        {
            get => navigationFrame;
            set => navigationFrame = value;
        }

        public GroupsDrawer()
        {
            this.InitializeComponent();
            groupService = App.Services.GetService<IGroupService>();

            controller = App.Services.GetService<AppController>();
            CreateGroupButton.Click += CreateGroup_Click; // Handle click event
            LoadGroups();
        }

        private void LoadGroups()
        {
            GroupsList.Children.Clear(); // Clear old items

            //long userId;
            //if (controller.CurrentUser == null)
            //{
            //    userId = -1;
            //}
            //else
            //{
            //    userId = controller.CurrentUser.Id;
            //}
            var groups = groupService.GetGroups(controller.CurrentUser.Id);

            foreach (var group in groups)
            {
                // Create a Button for each group to make it clickable
                var groupButton = new Button
                {
                    Tag = group.Id, // Store GroupId in Tag for navigation
                    Margin = new Thickness(0, 0, 0, 10),
                    Background = new SolidColorBrush(Colors.Transparent), // Match drawer style
                    BorderThickness = new Thickness(0),
                    Padding = new Thickness(5)
                };

                var groupItem = new StackPanel { Orientation = Orientation.Vertical };

                var groupHeader = new StackPanel { Orientation = Orientation.Horizontal };
                groupHeader.Children.Add(new TextBlock
                {
                    Text = "★",
                    Foreground = new SolidColorBrush(Colors.Gold),
                    FontSize = 18,
                    VerticalAlignment = VerticalAlignment.Center
                });
                groupHeader.Children.Add(new TextBlock
                {
                    Text = group.Name,
                    FontSize = 18,
                    Foreground = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(5, 0, 0, 0)
                });

                var groupDescription = new TextBlock
                {
                    Text = group.Description,
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Colors.Gray),
                    Margin = new Thickness(23, 0, 0, 0)
                };

                groupItem.Children.Add(groupHeader);
                groupItem.Children.Add(groupDescription);

                groupButton.Content = groupItem;
                groupButton.Click += GroupButton_Click; // Handle click event

                GroupsList.Children.Add(groupButton);
            }
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.Navigate(typeof(GroupPage), (long)((Button)sender).Tag);
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.Navigate(typeof(CreateGroup));
        }
    }

    // Ensure Group class has an Id property
    public class Group
    {
        public int Id { get; set; } // Added for navigation
        public string Name { get; set; }
        public string Description { get; set; }
    }
}