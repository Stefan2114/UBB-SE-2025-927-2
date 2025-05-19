using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Server.Interfaces
{
    public interface IMacrosController
    {
        ActionResult<List<Macros>> GetAllMacros();
        ActionResult<Macros> GetMacrosById(long id);
        ActionResult<List<Macros>> GetMacrosByUserId(long userId);
        IActionResult SaveMacros(Macros macros);
        IActionResult UpdateMacros(long id, Macros macros);
        IActionResult DeleteMacros(long id);

        // New endpoints for macro values by user
        ActionResult<double> GetProteinIntake(long userId);
        ActionResult<double> GetCarbohydratesIntake(long userId);
        ActionResult<double> GetFatIntake(long userId);
        ActionResult<double> GetFiberIntake(long userId);
        ActionResult<double> GetSugarIntake(long userId);
    }
}
