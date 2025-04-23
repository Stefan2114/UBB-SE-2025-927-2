using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using AppCommonClasses.Models;

namespace SocialApp.Components
{
    public sealed partial class GroupComponent : UserControl
    {
        public GroupComponent(AppCommonClasses.Models.Group group)
        {
            this.InitializeComponent();
            GroupName.Text = group.Name;
            GroupDescription.Text = group.Description;
            // Add more properties as needed
        }
    }
}
