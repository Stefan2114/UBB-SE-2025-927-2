using AppCommonClasses.Models;
using AppCommonClasses.Interfaces;
using System.Linq;

namespace AppCommonClasses.Services
{
    public class MacrosService : IMacrosService
    {
        private readonly IMacrosRepository _macrosRepository;

        public MacrosService(IMacrosRepository macrosRepository)
        {
            _macrosRepository = macrosRepository;
        }

        public double GetProteinIntake(long userId)
        {
            var macrosList = _macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalProtein ?? 0);
        }

        public double GetCarbohydratesIntake(long userId)
        {
            var macrosList = _macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalCarbohydrates ?? 0);
        }

        public double GetFatIntake(long userId)
        {
            var macrosList = _macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalFat ?? 0);
        }

        public double GetFiberIntake(long userId)
        {
            var macrosList = _macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalFiber ?? 0);
        }

        public double GetSugarIntake(long userId)
        {
            var macrosList = _macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalSugar ?? 0);
        }
    }
}
