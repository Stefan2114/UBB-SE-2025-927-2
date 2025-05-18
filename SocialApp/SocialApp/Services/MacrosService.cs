using SocialApp.Proxies;
using AppCommonClasses.Models;
using System.Linq; 

namespace SocialApp.Services
{
    public class MacrosService
    {
        private readonly MacrosRepositoryProxy _macrosRepo;

        // Constructor to initialize the proxy repository
        public MacrosService()
        {
            _macrosRepo = new MacrosRepositoryProxy();
        }

        // Get protein intake using the repository proxy
        public double GetProteinIntake(long userId)
        {
            var macrosList = _macrosRepo.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalProtein ?? 0);
        }

        // Get carbohydrates intake using the repository proxy
        public double GetCarbohydratesIntake(long userId)
        {
            var macrosList = _macrosRepo.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalCarbohydrates ?? 0);
        }

        // Get fat intake using the repository proxy
        public double GetFatIntake(long userId)
        {
            var macrosList = _macrosRepo.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalFat ?? 0);
        }

        // Get fiber intake using the repository proxy
        public double GetFiberIntake(long userId)
        {
            var macrosList = _macrosRepo.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalFiber ?? 0);
        }

        // Get sugar intake using the repository proxy
        public double GetSugarIntake(long userId)
        {
            var macrosList = _macrosRepo.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalSugar ?? 0);
        }
    }
}
