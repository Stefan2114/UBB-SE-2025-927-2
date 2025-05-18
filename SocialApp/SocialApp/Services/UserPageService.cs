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

        public long UserHasAnAccount(string name)
        {
            User? user = this.userServiceProxy.GetUserByUsername(name);

            return user == null ? -1 : user.Id;
        }

        public long InsertNewUser(string name, string password)
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

            return createdUser == null ? -1 : createdUser.Id;
        }
    }
}