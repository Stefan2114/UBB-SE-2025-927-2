namespace SocialApp.Services
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;
    using SocialApp.Queries;
    using System;
    using System.Data.SqlClient;

    /// <summary>
    /// Service for managing body metrics of users.
    /// </summary>
    public class BodyMetricService : IBodyMetricService
    {
        private readonly IDataLink dataLink;

        /// <summary>
        /// Constructor that takes a data link dependency.
        /// </summary>
        public BodyMetricService(IDataLink ddataLink)
        {
            dataLink = ddataLink;
        }

        public BodyMetricService()
        {
            dataLink = DataLink.Instance;
        }

        /// <summary>
        /// Updates the body metrics of a user in the database.
        /// </summary>
        [Obsolete]
        public void UpdateUserBodyMetrics(string firstName, string lastName, string weight, string height, string targetGoal)
        {
            float userWeight = float.Parse(weight);
            float userHeight = float.Parse(height);
            float? userTargetGoal = string.IsNullOrWhiteSpace(targetGoal) ? null : float.Parse(targetGoal);

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", $"{lastName} {firstName}"),
            };

            int userId = dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", userId),
                new SqlParameter("@u_weight", userWeight),
                new SqlParameter("@u_height", userHeight),
                new SqlParameter("@u_goal", userTargetGoal ?? (object)DBNull.Value),
            };

            dataLink.ExecuteNonQuery("UpdateUserBodyMetrics", parameters);
        }
    }
}
