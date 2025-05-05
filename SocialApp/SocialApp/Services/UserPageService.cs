using AppCommonClasses.DTOs;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using SocialApp.Interfaces;
using SocialApp.Proxies;
using SocialApp.Queries;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SocialApp.Services
{
    public class UserPageService : IUserPageService
    {
        private IUserRepository userRepository;

        public UserPageService()
        {
            this.userRepository =  new UserRepositoryProxy();
        }

        public int UserHasAnAccount(string name)
        {
            //var parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@u_name", name)
            //};

            //int? userId = _dataLink.ExecuteScalar<int>("SELECT dbo.GetUserByName(@u_name)", parameters, false);

            //return userId.HasValue && userId.Value > 0 ? userId.Value : -1;
            //get all users and print them in the console
            var users = this.userRepository.GetAll();
            foreach (var usera in users)
            {
                Debug.WriteLine($"User: {usera.Name}");
            }
            UserModel user = this.userRepository.GetByUsername(name);
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
            //var parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@u_name", name)
            //};
            //int userId = _dataLink.ExecuteScalar<int>("SELECT dbo.InsertNewUser(@u_name)", parameters, false);
            //return userId;
            UserModel user = new UserModel
            {
                Name = name,
                Height = 0,
                Weight = 0,
                TargetWeight = null,
                GoalId = null,
                CookingSkillId = null,
                DietaryPreferenceId = null,
                AllergyId = null,
                ActivityLevelId = null
            };
            this.userRepository.Save(user);
            return (int)user.Id;
        }
    }
}
