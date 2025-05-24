using AppCommonClasses.Models;
using AppCommonClasses.Interfaces;
using System.Linq;

namespace AppCommonClasses.Services
{
    //we need to calculate the total macros for a person by this formula:
    // basal_metabolism_rate = 10 * weight + 5 * height
    // if female: basal_metabolism_rate -= 200
    // total_daily_energy_expenditure = basal_metabolism_rate * activity_level_multyplier (1.2, 1.4, 1.6, 1,7, 1.9)
    // if goal == "lose weight": calories = total_daily_energy_expenditure - 500
    // if goal == "gain muscles": calories = total_daily_energy_expenditure + 500
    // if goal == "maintain": calories = total_daily_energy_expenditure
    // protein = 30%
    // carbohydrates = 40%
    // fat = 30%
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
