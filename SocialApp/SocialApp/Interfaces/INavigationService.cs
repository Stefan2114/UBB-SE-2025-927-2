using System;

namespace SocialApp.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo(Type pageType, object parameter = null);
        void GoBack();
    }
}
