using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface IGoalPageRepository
    {
        void AddGoals(string userName, string g_description);
    }
}
