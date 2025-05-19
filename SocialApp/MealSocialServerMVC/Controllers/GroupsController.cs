using Microsoft.AspNetCore.Mvc;
using MealSocialServerMVC.Models;
using AppCommonClasses.Services;
using AppCommonClasses.Interfaces;
using AppCommonClasses.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppCommonClasses.Data;
using Microsoft.EntityFrameworkCore;
using AppCommonClasses.Models;
using CommonGroup = AppCommonClasses.Models.Group;

namespace MealSocialServerMVC.Controllers
{
    [Route("groups")]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var groups = _groupService.GetAllGroups();
            return View(groups);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CommonGroup group)
        {
            if (ModelState.IsValid)
            {
                _groupService.AddGroup(group.Name, group.Description, group.Image, group.AdminId);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var group = _groupService.GetGroupById(id);
            if (group == null) return NotFound();
            return View(group);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CommonGroup group)
        {
            if (ModelState.IsValid)
            {
                _groupService.UpdateGroup(group.Id, group.Name, group.Description, group.Image, group.AdminId);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var group = _groupService.GetGroupById(id);
            if (group == null) return NotFound();
            return View(group);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _groupService.DeleteGroup(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 