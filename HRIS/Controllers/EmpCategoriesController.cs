using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;
using HRIS.Models;

namespace HRIS.Controllers
{
    public class EmpCategoriesController : Controller
    {
        private readonly HrDbContext _context;

        public EmpCategoriesController(HrDbContext context)
        {
            _context = context;
        }

        // GET: EmpCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpCategories.ToListAsync());
        }

        // GET: EmpCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empCategory = await _context.EmpCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empCategory == null)
            {
                return NotFound();
            }

            return View(empCategory);
        }

        // GET: EmpCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Closed,Notes")] EmpCategory empCategory)
        {
            if (ModelState.IsValid)
            {
                empCategory.Id = Guid.NewGuid();
                _context.Add(empCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empCategory);
        }

        // GET: EmpCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empCategory = await _context.EmpCategories.FindAsync(id);
            if (empCategory == null)
            {
                return NotFound();
            }
            return View(empCategory);
        }

        // POST: EmpCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Closed,Notes")] EmpCategory empCategory)
        {
            if (id != empCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpCategoryExists(empCategory.Id))
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
            return View(empCategory);
        }

        // GET: EmpCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empCategory = await _context.EmpCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empCategory == null)
            {
                return NotFound();
            }

            return View(empCategory);
        }

        // POST: EmpCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var empCategory = await _context.EmpCategories.FindAsync(id);
            _context.EmpCategories.Remove(empCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpCategoryExists(Guid id)
        {
            return _context.EmpCategories.Any(e => e.Id == id);
        }
    }
}
