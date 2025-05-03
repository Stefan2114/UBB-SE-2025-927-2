using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
