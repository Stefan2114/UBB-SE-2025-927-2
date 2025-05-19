using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using AppCommonClasses.Services;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("macros")]
    public class MacrosController : ControllerBase, IMacrosController
    {
        private readonly IMacrosService _macrosService;
        private readonly IMacrosRepository _macrosRepository;

        public MacrosController(IMacrosService macrosService, IMacrosRepository macrosRepository)
        {
            _macrosService = macrosService;
            _macrosRepository = macrosRepository;
        }

        [HttpGet]
        public ActionResult<List<Macros>> GetAllMacros()
        {
            return _macrosRepository.GetAllMacros();
        }

        [HttpGet("{id}")]
        public ActionResult<Macros> GetMacrosById(long id)
        {
            return _macrosRepository.GetMacrosById(id);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Macros>> GetMacrosByUserId(long userId)
        {
            return _macrosRepository.GetMacrosByUserId(userId);
        }

        [HttpGet("user/{userId}/protein")]
        public ActionResult<double> GetProteinIntake(long userId)
        {
            var protein = _macrosService.GetProteinIntake(userId);
            return Ok(protein);
        }

        [HttpGet("user/{userId}/carbohydrates")]
        public ActionResult<double> GetCarbohydratesIntake(long userId)
        {
            var carbs = _macrosService.GetCarbohydratesIntake(userId);
            return Ok(carbs);
        }

        [HttpGet("user/{userId}/fat")]
        public ActionResult<double> GetFatIntake(long userId)
        {
            var fat = _macrosService.GetFatIntake(userId);
            return Ok(fat);
        }

        [HttpGet("user/{userId}/fiber")]
        public ActionResult<double> GetFiberIntake(long userId)
        {
            var fiber = _macrosService.GetFiberIntake(userId);
            return Ok(fiber);
        }

        [HttpGet("user/{userId}/sugar")]
        public ActionResult<double> GetSugarIntake(long userId)
        {
            var sugar = _macrosService.GetSugarIntake(userId);
            return Ok(sugar);
        }

        [HttpPost]
        public IActionResult SaveMacros(Macros macros)
        {
            _macrosRepository.SaveMacros(macros);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMacros(long id, Macros macros)
        {
            _macrosRepository.UpdateMacrosById(id, macros);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMacros(long id)
        {
            _macrosRepository.DeleteMacrosById(id);
            return Ok();
        }
    }
}
