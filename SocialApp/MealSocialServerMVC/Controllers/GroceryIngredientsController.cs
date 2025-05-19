using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCommonClasses.Models;
using MealSocialServerMVC.Data;
using SocialApp.Interfaces;

namespace MealSocialServerMVC.Controllers
{
    public class GroceryIngredientsController : Controller
    {
        private readonly IGroceryListService _groceryListService;

        public GroceryIngredientsController(IGroceryListService groceryListService)
        {
            _groceryListService = groceryListService;
        }

        // GET: GroceryIngredients
        public async Task<IActionResult> Index()
        {
            var groceryIngredients = await _groceryListService.GetIngredientsForUser(1); // Replace with actual user ID
            return View(groceryIngredients);
        }

        // GET: GroceryIngredients/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _groceryListService.GetIngredientsForUser(1);
            var groceryIngredient = ingredients?.FirstOrDefault(i => i.Id == id);
            if (groceryIngredient == null)
            {
                return NotFound();
            }

            return View(groceryIngredient);
        }

        // GET: GroceryIngredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroceryIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IngredientId,IsChecked")] GroceryIngredient groceryIngredient)
        {
            if (ModelState.IsValid)
            {
                await _groceryListService.AddIngredientToUser(1, groceryIngredient, groceryIngredient.Name, null);
                return RedirectToAction(nameof(Index));
            }
            return View(groceryIngredient);
        }

        // GET: GroceryIngredients/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _groceryListService.GetIngredientsForUser(1);
            var groceryIngredient = ingredients?.FirstOrDefault(i => i.Id == id);
            if (groceryIngredient == null)
            {
                return NotFound();
            }
            return View(groceryIngredient);
        }

        // POST: GroceryIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IngredientId,IsChecked")] GroceryIngredient groceryIngredient)
        {
            if (id != groceryIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _groceryListService.UpdateIsChecked(1, groceryIngredient.IngredientId, groceryIngredient.IsChecked);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GroceryIngredientExists(groceryIngredient.Id))
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
            return View(groceryIngredient);
        }

        // GET: GroceryIngredients/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _groceryListService.GetIngredientsForUser(1);
            var groceryIngredient = ingredients?.FirstOrDefault(i => i.Id == id);
            if (groceryIngredient == null)
            {
                return NotFound();
            }

            return View(groceryIngredient);
        }

        // POST: GroceryIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var ingredients = await _groceryListService.GetIngredientsForUser(1);
            var groceryIngredient = ingredients?.FirstOrDefault(i => i.Id == id);
            if (groceryIngredient != null)
            {
                await _groceryListService.UpdateIsChecked(1, groceryIngredient.IngredientId, false);
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GroceryIngredientExists(long id)
        {
            var ingredients = await _groceryListService.GetIngredientsForUser(1);
            return ingredients?.Any(e => e.Id == id) ?? false;
        }
    }
}
