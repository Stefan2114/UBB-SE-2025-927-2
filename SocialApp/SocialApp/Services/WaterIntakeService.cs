namespace SocialApp.Services
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using AppCommonClasses.Interfaces;
    using SocialApp.Interfaces;
    using SocialApp.Queries;

    public class WaterIntakeService : IWaterIntakeService
    {
        public IDataLink dataLink;

        [Obsolete]
        public WaterIntakeService()
        {
            dataLink = DataLink.Instance;
        }

        [Obsolete]
        public void AddUserIfNotExists(long userId)
        {
            // if the current user not in the water_tracker table, add it
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            var result = dataLink.ExecuteScalar<int>("SELECT COUNT(*) FROM water_trackers WHERE u_id = @UserId", parameters, false);
            if (result == 0)
            {
                // add the user to the water_tracker table
                var insertParameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
                Debug.WriteLine(userId);
                dataLink.ExecuteQuery("INSERT INTO water_trackers (u_id, water_intake, water_goal) VALUES (@UserId, 0, 2000)", insertParameters, false);
            }
        }

        [Obsolete]
        public float GetWaterIntake(long userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return dataLink.ExecuteScalar<float>("SELECT dbo.get_water_intake(@UserId)", parameters, false);
        }

        [Obsolete]
        public void UpdateWaterIntake(long userId, float newIntake)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@NewIntake", newIntake),
            };
            dataLink.ExecuteQuery("exec update_water_intake @UserId, @NewIntake", parameters, false);
        }

        [Obsolete]
        public void RemoveWater300(long userId)
        {
            const float WATER_DIFFERENCE_300 = 300f;
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_300); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }

        [Obsolete]
        public void RemoveWater400(long userId)
        {
            const float WATER_DIFFERENCE_400 = 400f;
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_400); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }

        [Obsolete]
        public void RemoveWater500(long userId)
        {
            const float WATER_DIFFERENCE_500 = 500f;
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_500); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }

        [Obsolete]
        public void RemoveWater750(long userId)
        {
            const float WATER_DIFFERENCE_750 = 750f;
            float currentIntake = GetWaterIntake(userId);
            float newIntake = Math.Max(0, currentIntake - WATER_DIFFERENCE_750); // Ensure we don't go below 0
            UpdateWaterIntake(userId, newIntake);
        }
    }
}
