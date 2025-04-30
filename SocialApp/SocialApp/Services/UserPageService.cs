using AppCommonClasses.Interfaces;
using SocialApp.Interfaces;
using SocialApp.Queries;
using System.Data;
using System.Data.SqlClient;

namespace SocialApp.Services
{
    public class UserPageService : IUserPageService
    {
        private readonly IDataLink _dataLink;

        public UserPageService()
        {
            _dataLink = DataLink.Instance;
        }

        public UserPageService(IDataLink dataLink)
        {
            _dataLink = dataLink;
        }

        public int UserHasAnAccount(string name)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", name)
            };

            int? userId = _dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            return userId.HasValue && userId.Value > 0 ? userId.Value : -1;
        }

        public int InsertNewUser(string name)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@u_name", name),
                new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            _dataLink.ExecuteNonQuery("InsertNewUser", parameters);
            return (int)parameters[1].Value;
        }
    }
}
