namespace MealPlannerProject.Converters
{
    using System;
    using MealPlannerProject;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Data;

    public class SelectedGoalOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string selectedGoal = (string)parameter;
            return value?.ToString() == selectedGoal ? 0.5 : 1.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
