using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCommonClasses.Data;
using AppCommonClasses.Models;
using AppCommonClasses.Interfaces;
using System.Diagnostics;

namespace MealSocialServerMVC.Controllers
{
    [Route("water")]
    public class WatersController : Controller
    {
        private readonly IWaterIntakeService waterIntakeService;

        public WatersController(IWaterIntakeService waterIntakeService)
        {
            this.waterIntakeService = waterIntakeService;
        }

        // GET: Waters
        [HttpGet]
        public IActionResult Index()
        {
            waterIntakeService.AddUserIfNotExists(1);
            var intake = waterIntakeService.GetWaterIntake(1);
            ViewBag.CurrentIntake = intake;
            Debug.WriteLine($"Water intake for user 1: {intake}");
            return View();
        }

        [HttpPost]
        [Route("waters/AddWater")]
        public IActionResult AddWater(float amount)
        {
            Debug.WriteLine("lalalalalala adding*******************");
            waterIntakeService.AddUserIfNotExists(1);
            var intake = waterIntakeService.GetWaterIntake(1);
            waterIntakeService.UpdateWaterIntake(1, intake + amount);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("water/RemoveWater")]
        public IActionResult RemoveWater(float amount)
        {
            Debug.WriteLine("lalalalalala deleting*******************");
            switch (amount)
            {
                case 0.3f:
                    waterIntakeService.RemoveWater300(1);
                    break;
                case 0.4f:
                    waterIntakeService.RemoveWater400(1);
                    break;
                case 0.5f:
                    waterIntakeService.RemoveWater500(1);
                    break;
                case 0.75f:
                    waterIntakeService.RemoveWater750(1);
                    break;
            }

            return RedirectToAction("Index");
        }

        // GET: Waters/Details/5
        /*[HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.WaterTrackers
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.U_Id == id);
            if (water == null)
            {
                return NotFound();
            }

            return View(water);
        }

        // GET: Waters/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Waters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("U_Id,water_intake")] Water water)
        {
            if (ModelState.IsValid)
            {
                _context.Add(water);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email", water.U_Id);
            return View(water);
        }

        // GET: Waters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.WaterTrackers.FindAsync(id);
            if (water == null)
            {
                return NotFound();
            }
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email", water.U_Id);
            return View(water);
        }

        // POST: Waters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("U_Id,water_intake")] Water water)
        {
            if (id != water.U_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(water);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaterExists(water.U_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["U_Id"] = new SelectList(_context.Users, "Id", "Email", water.U_Id);
            return View(water);
        }

        // GET: Waters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.WaterTrackers
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.U_Id == id);
            if (water == null)
            {
                return NotFound();
            }

            return View(water);
        }

        // POST: Waters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var water = await _context.WaterTrackers.FindAsync(id);
            if (water != null)
            {
                _context.WaterTrackers.Remove(water);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaterExists(long id)
        {
            return _context.WaterTrackers.Any(e => e.U_Id == id);
        }*/
    }
}
