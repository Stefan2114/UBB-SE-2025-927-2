namespace MealPlannerProject.Services
{
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using MealPlannerProject.Queries;
    using SocialApp.Interfaces;

    internal class ActivityPageService : IActivityPageService
    {
        private const string UserLookupQuery = "SELECT dbo.GetUserByName(@userFullName)";
        private const string ActivityLookupQuery = "SELECT dbo.GetActivityByDescription(@activityDescription)";
        private const string UpdateActivityProcedure = "UpdateUserActivity";

        private const string UserNameParameter = "@userFullName";
        private const string ActivityDescriptionParameter = "@activityDescription";
        private const string UserIdParameter = "@u_id";
        private const string ActivityIdParameter = "@a_id";

        private const bool IsDirectSqlQuery = false;

        [System.Obsolete]
        public void AddActivity(string firstName, string lastName, string activityDescription)
        {
            Debug.WriteLine($"Adding activity {activityDescription} for user {firstName} {lastName}");

            string userFullName = lastName + " " + firstName;
            Debug.WriteLine($"User name: {userFullName}");

            var userLookupParameters = new SqlParameter[]
            {
               new SqlParameter(UserNameParameter, userFullName),
            };

            int userId = DataLink.Instance.ExecuteScalar<int>(
                UserLookupQuery,
                userLookupParameters,
                IsDirectSqlQuery);

            Debug.WriteLine($"User ID: {userId}");

            var activityLookupParameters = new SqlParameter[]
            {
                new SqlParameter(ActivityDescriptionParameter, activityDescription),
            };

            int activityId = DataLink.Instance.ExecuteScalar<int>(
                ActivityLookupQuery,
                activityLookupParameters,
                IsDirectSqlQuery);

            var updateParameters = new SqlParameter[]
            {
                new SqlParameter(UserIdParameter, userId),
                new SqlParameter(ActivityIdParameter, activityId),
            };

            DataLink.Instance.ExecuteNonQuery(UpdateActivityProcedure, updateParameters);
            Debug.WriteLine($"Successfully updated activity level to '{activityDescription}' for user {firstName} {lastName}");
        }

        public static bool ValidateSelectedActivity(string activity)
        {
            return !string.IsNullOrWhiteSpace(activity);
        }

        public static ObservableCollection<string> GetActivityLevels()
        {
            return new ObservableCollection<string>
                {
                "Sedentary",
                "Lightly Active",
                "Moderately Active",
                "Very Active",
                "Super Active",
                };
        }
    }
}