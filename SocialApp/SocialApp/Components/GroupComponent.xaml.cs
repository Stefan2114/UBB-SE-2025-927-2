using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialApp.Entities;

namespace SocialApp.Components
{
    public sealed partial class GroupComponent : UserControl
    {
        public GroupComponent(SocialApp.Entities.Group group)
        {
            this.InitializeComponent();
            GroupName.Text = group.Name;
            GroupDescription.Text = group.Description;
            // Add more properties as needed
        }
    }
}
