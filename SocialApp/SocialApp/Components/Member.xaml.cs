using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SocialApp.Pages;
using SocialApp.Repository;
using SocialApp.Services;
using SocialApp.Entities;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialApp.Components
{
    public sealed partial class Member : UserControl
    {
        private readonly User member;
        private readonly AppController controller;
        private readonly Frame navigationFrame;
        private readonly long groupId;
        private readonly bool isAdmin;

        public Member(User member, Frame frame, long groupId, bool isAdmin)
        {
            this.InitializeComponent();
            this.member = member;
            this.controller = App.Services.GetService<AppController>();
            this.navigationFrame = frame;
            this.groupId = groupId;
            this.isAdmin = isAdmin;

            MemberName.Text = member.Username;
            SetImage();
            if (isAdmin && member.Id != controller.CurrentUser.Id) // Don’t show Remove for self
                RemoveButton.Visibility = Visibility.Visible;

            this.PointerPressed += Member_Click;
        }

        private async void SetImage()
        {
            if (!string.IsNullOrEmpty(member.Image))
                MemberImage.Source = await AppController.DecodeBase64ToImageAsync(member.Image);
        }

        private void Member_Click(object sender, RoutedEventArgs e)
        {
            navigationFrame.Navigate(typeof(UserPage), new UserPageNavigationArgs(controller, member));
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            //var groupService = new GroupService(new GroupRepository(), new UserRepository());
            //groupService.RemoveMember(groupId, member.Id);
            //this.Visibility = Visibility.Collapsed; // Hide the member locally
        }
    }
}
