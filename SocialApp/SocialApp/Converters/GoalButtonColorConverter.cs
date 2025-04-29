namespace MealPlannerProject.Converters
{
    using System;
    using Microsoft.UI;
    using Microsoft.UI.Xaml.Data;
    using Microsoft.UI.Xaml.Media;
    using Windows.UI;

    public class GoalButtonColorConverter : IValueConverter
    {
        private const byte FullyOpaqueAlpha = 255;

        private const string WeightLossGoal = "Lose weight";
        private const string WeightGainGoal = "Gain weight";
        private const string WeightMaintenanceGoal = "Maintain weight";
        private const string BodyRecompositionGoal = "Body recomposition";
        private const string HealthImprovementGoal = "Improve overall health";

        private static readonly Color WeightLossColor = Color.FromArgb(FullyOpaqueAlpha, 82, 115, 91);      // Green shade
        private static readonly Color WeightGainColor = Color.FromArgb(FullyOpaqueAlpha, 186, 104, 72);     // Brown-orange shade
        private static readonly Color WeightMaintenanceColor = Color.FromArgb(FullyOpaqueAlpha, 25, 50, 38); // Dark green shade
        private static readonly Color BodyRecompositionColor = Color.FromArgb(FullyOpaqueAlpha, 238, 217, 195); // Light beige shade
        private static readonly Color HealthImprovementColor = Color.FromArgb(FullyOpaqueAlpha, 176, 135, 94); // Tan shade
        private static readonly Color DefaultColor = Colors.White;

        public object Convert(object goalValue, Type targetType, object parameterValue, string cultureInfo)
        {
            string goalType = goalValue?.ToString() ?? string.Empty;

            return goalType switch
            {
                WeightLossGoal => new SolidColorBrush(WeightLossColor),
                WeightGainGoal => new SolidColorBrush(WeightGainColor),
                WeightMaintenanceGoal => new SolidColorBrush(WeightMaintenanceColor),
                BodyRecompositionGoal => new SolidColorBrush(BodyRecompositionColor),
                HealthImprovementGoal => new SolidColorBrush(HealthImprovementColor),
                _ => new SolidColorBrush(DefaultColor)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException("Converting colors back to goal types is not supported.");
        }
    }
}