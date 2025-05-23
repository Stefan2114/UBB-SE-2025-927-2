using AppCommonClasses.Models;
using System.Collections.Generic;

namespace AppCommonClasses.Services
{
    public interface IMacrosService
    {
        List<Macros> GetMacrosListByUserId(long userId);
        double GetProteinIntake(long userId);
        double GetCarbohydratesIntake(long userId);
        double GetFatIntake(long userId);
        double GetFiberIntake(long userId);
        double GetSugarIntake(long userId);
    }
}
