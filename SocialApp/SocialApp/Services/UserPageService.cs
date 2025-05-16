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
            User? user = this.userServiceProxy.GetUserByUsername(name);

            if (user != null)
            {
                return (int)user.Id;
            }
            else
            {
                return -1;
            }
        }

        public int InsertNewUser(string name, string password)
        {
            User user = new User
            {
                Username = name,
                Password = password,
                Height = 0,
                Weight = 0,
                TargetWeight = 0,
                GoalId = null,
                CookingSkillId = null,
                DietaryPreferenceId = null,
                AllergyId = null,
                ActivityLevelId = null,
            };

            var createdUser = this.userServiceProxy.Save(user);
            if (createdUser == null)
            {
                Debug.WriteLine("Error getting saved user");
                return -1;
            }

            return (int)createdUser.Id;
        }
    }
}