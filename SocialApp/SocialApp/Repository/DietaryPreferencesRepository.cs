using AppCommonClasses.Interfaces;
using SocialApp.Queries;
using System;
using System.Data.SqlClient;

namespace SocialApp.Repository
{
    public class DietaryPreferencesRepository : IDietaryPreferencesRepository
    {
        private readonly IDataLink dataLink;

        public DietaryPreferencesRepository()
        {
            dataLink = DataLink.Instance;
        }

        public DietaryPreferencesRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        [Obsolete]
        public void AddAllergyAndDietaryPreference(string username, string dietaryPreference, string allergy)
        {
            // this uses the sql update function
            var parameters = new SqlParameter[]
            {
                new ("@u_name", username),
            };
            int u_id = dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);
            parameters =
            [
                new SqlParameter("@dp_description", dietaryPreference),
            ];
            int dp_id = dataLink.ExecuteScalar<int>("SELECT dbo.GetDietaryPreferencesByDescription(@dp_description)", parameters, false);
            parameters =
            [
                new SqlParameter("@a_description", allergy),
            ];
            int a_id = dataLink.ExecuteScalar<int>("SELECT dbo.GetAllergiesByDescription(@a_description)", parameters, false);
            parameters =
            [
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@dp_id", dp_id),
            ];
            dataLink.ExecuteNonQuery("UpdateUserDietaryPreference", parameters);
            parameters =
            [
                new SqlParameter("@u_id", u_id),
                new SqlParameter("@a_id", a_id),
            ];
            dataLink.ExecuteNonQuery("UpdateUserAllergies", parameters);
        }
    }
}
