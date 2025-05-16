// using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using SocialApp.Queries;

namespace SocialApp.Services
{
    class MacrosService
    {
        public float GetProteinIntake(long userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_protein_food(@UserId)", parameters, false);
        }

        public float GetCarbohydratesIntake(long userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_carbohydrates_food(@UserId)", parameters, false);
        }

        public float GetFatIntake(long userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_fat_food(@UserId)", parameters, false);
        }

        public float GetFiberIntake(long userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_fiber_food(@UserId)", parameters, false);
        }

        public float GetSugarIntake(long userId)
        {
            var parameters = new SqlParameter[] { new SqlParameter("@UserId", userId) };
            return DataLink.Instance.ExecuteScalar<float>("SELECT dbo.get_sugar_food(@UserId)", parameters, false);
        }

    }
}