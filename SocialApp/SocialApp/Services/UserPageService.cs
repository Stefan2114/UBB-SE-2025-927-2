using System.Diagnostics;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using SocialApp.Interfaces;
using SocialApp.Proxies;

namespace SocialApp.Services
{
    public class UserPageService : IUserPageService
    {
        private IUserService userServiceProxy;

        public UserPageService()
        {
            this.userServiceProxy = new UserServiceProxy();
        }

        public int UserHasAnAccount(string name)
        {
            // var parameters = new SqlParameter[]
            // {
            //    new SqlParameter("@u_name", name)
            // };

            // int? userId = _dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            // return userId.HasValue && userId.Value > 0 ? userId.Value : -1;
            // get all users and print them in the console
            var users = this.userServiceProxy.GetAllUsers();
            foreach (var user1 in users)
            {
                Debug.WriteLine($"User: {user1.Username}");
            }

            User user = this.userServiceProxy.GetUserByUsername(name);
            if (user != null)
            {
                return (int)user.Id;
            }
            else
            {
                return -1;
            }
        }

        public int InsertNewUser(string name)
        {
            // var parameters = new SqlParameter[]
            // {
            //    new SqlParameter("@u_name", name)
            // };
            // int userId = _dataLink.ExecuteScalar<int>("SELECT dbo.InsertNewUser(@u_name)", parameters, false);
            // return userId;
            User user = new User
            {
                Username = name,
                Height = 0,
                Weight = 0,
                TargetWeight = 0,
                GoalId = null,
                CookingSkillId = null,
                DietaryPreferenceId = null,
                AllergyId = null,
                ActivityLevelId = null,
            };
            this.userServiceProxy.Save(user);
            return (int)user.Id;
        }
    }
}