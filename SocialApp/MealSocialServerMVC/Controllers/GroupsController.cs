using Microsoft.AspNetCore.Mvc;
using MealSocialServerMVC.Models;
using MealSocialServerMVC.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MealSocialServerMVC.Controllers
{
    public class GroupsController : Controller
    {
        private readonly GroupApiService _groupApiService;

        public GroupsController(GroupApiService groupApiService)
        {
            _groupApiService = groupApiService;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupApiService.GetAllGroupsAsync();
            return View(groups);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group group)
        {
            if (ModelState.IsValid)
            {
                await _groupApiService.CreateGroupAsync(group);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var group = await _groupApiService.GetGroupByIdAsync(id);
            if (group == null) return NotFound();
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                await _groupApiService.UpdateGroupAsync(group);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var group = await _groupApiService.GetGroupByIdAsync(id);
            if (group == null) return NotFound();
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _groupApiService.DeleteGroupAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 