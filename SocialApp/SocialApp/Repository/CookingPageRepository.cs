namespace SocialApp.Repository
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Queries;
    using System;
    using System.Data.SqlClient;

    public class CookingPageRepository : ICookingPageRepository
    {
        private readonly IDataLink dataLink;

        [Obsolete]
        public CookingPageRepository()
        {
            dataLink = DataLink.Instance;
        }

        public CookingPageRepository(IDataLink dataLink)
        {
            this.dataLink = dataLink;
        }

        [Obsolete]
        public int GetUserIdByName(string firstName, string lastName)
        {
            string fullName = lastName + " " + firstName;
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", fullName),
            };

            return dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);
        }

        [Obsolete]
        public int GetCookingSkillIdByDescription(string description)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@cs_description", description),
            };

            return dataLink.ExecuteScalar<int>("SELECT dbo.GetCookingSkillByDescription(@cs_description)", parameters, false);
        }

        [Obsolete]
        public void UpdateUserCookingSkill(int userId, int cookingSkillId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_id", userId),
                new SqlParameter("@cs_id", cookingSkillId),
            };

            dataLink.ExecuteNonQuery("UpdateUserCookingSkill", parameters);
        }
    }
}
