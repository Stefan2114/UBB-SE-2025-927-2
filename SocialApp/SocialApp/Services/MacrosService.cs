// using Microsoft.Data.SqlClient;
using SocialApp.Queries;
using System.Data.SqlClient;

namespace MealPlannerProject.Services
{
    class MacrosService
    {
        public float GetProteinIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_protein_food(@UserId)", parameters, false);
        }

        public float GetCarbohydratesIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_carbohydrates_food(@UserId)", parameters, false);
        }

        public float GetFatIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_fat_food(@UserId)", parameters, false);
        }

        public float GetFiberIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_fiber_food(@UserId)", parameters, false);
        }

        public float GetSugarIntake(int userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_sugar_food(@UserId)", parameters, false);
        }

    }
}