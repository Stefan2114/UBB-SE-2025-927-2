using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;

namespace MealPlannerProject.Converters
{
    public class CookingLevelButtonStyleConverter : IValueConverter
    {
        private static readonly Dictionary<string, string> CookingLevelButtonStyles = new Dictionary<string, string>
        {
            { "I'm a beginner", "BeginnerButtonStyle" },
            { "I cook sometimes", "CookSometimesButtonStyle" },
            { "I love cooking", "LoveCookingButtonStyle" },
            { "I prefer quick meals", "QuickMealsButtonStyle" },
            { "I meal prep", "MealPrepButtonStyle" }
        };

        private readonly Dictionary<string, object>? _testResources;

        public CookingLevelButtonStyleConverter(Dictionary<string, object>? testResources = null)
        {
            _testResources = testResources;
        }

        public CookingLevelButtonStyleConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string cookingLevel && CookingLevelButtonStyles.ContainsKey(cookingLevel))
            {
                var styleName = CookingLevelButtonStyles[cookingLevel];
                return Application.Current.Resources[styleName] as Style;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
