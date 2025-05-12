using SocialApp.Interfaces;
using SocialApp.Proxies;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialApp.ViewModels
{
    public class UserPageViewModel
    {
        private IUserService userService;

        private string username = string.Empty;

        public UserPageViewModel(IUserService userService)
        {
            var userRepository = new UserRepositoryProxy();
            this.userService = new UserService(userRepository);
        }
    }
}
