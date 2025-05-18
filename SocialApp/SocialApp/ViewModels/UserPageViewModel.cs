namespace SocialApp.ViewModels
{
    using AppCommonClasses.Interfaces;
    using SocialApp.Proxies;

    public class UserPageViewModel
    {
        private IUserService userService;

        private string username = string.Empty;
        private string password = string.Empty;

        public UserPageViewModel(IUserService userService)
        {
            var userServiceProxy = new UserServiceProxy();
            this.userService = userServiceProxy;
        }
    }
}
