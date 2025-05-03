using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("macros")]
    public class MacrosController : ControllerBase, IMacrosController
    {
        private readonly IMacrosRepository macrosRepository;

        public MacrosController(IMacrosRepository macrosRepository)
        {
            this.macrosRepository = macrosRepository;
        }

        [HttpGet]
        public ActionResult<List<Macros>> GetAllMacros()
        {
            return this.macrosRepository.GetAllMacros();
        }

        [HttpGet("{id}")]
        public ActionResult<Macros> GetMacrosById(long id)
        {
            return this.macrosRepository.GetMacrosById(id);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Macros>> GetMacrosByUserId(long userId)
        {
            return this.macrosRepository.GetMacrosByUserId(userId);
        }

        [HttpPost]
        public IActionResult SaveMacros(Macros macros)
        {
            this.macrosRepository.SaveMacros(macros);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMacros(long id, Macros macros)
        {
            this.macrosRepository.UpdateMacrosById(id, macros);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMacros(long id)
        {
            this.macrosRepository.DeleteMacrosById(id);
            return Ok();
        }
    }
}
