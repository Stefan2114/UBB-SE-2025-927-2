using System;

namespace MealPlannerProject.Interfaces.Services
{
    public interface INavigationService
    {
        void NavigateTo(Type pageType, object parameter = null);
        void GoBack();
    }
}
