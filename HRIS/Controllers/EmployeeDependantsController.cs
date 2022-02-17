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
    public class EmployeeDependantsController : Controller
    {
        private readonly HrDbContext _context;

        public EmployeeDependantsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeDependants
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeDependants.ToListAsync());
        }

        // GET: EmployeeDependants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDependant = await _context.EmployeeDependants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDependant == null)
            {
                return NotFound();
            }

            return View(employeeDependant);
        }

        // GET: EmployeeDependants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeDependants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeNo,Names,RelationShip,Gender,DOB,Notes,CreatedDate,ModifiedDate,Personnel")] EmployeeDependant employeeDependant)
        {
            if (ModelState.IsValid)
            {
                employeeDependant.Id = Guid.NewGuid();
                _context.Add(employeeDependant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDependant);
        }

        // GET: EmployeeDependants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDependant = await _context.EmployeeDependants.FindAsync(id);
            if (employeeDependant == null)
            {
                return NotFound();
            }
            return View(employeeDependant);
        }

        // POST: EmployeeDependants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EmployeeNo,Names,RelationShip,Gender,DOB,Notes,CreatedDate,ModifiedDate,Personnel")] EmployeeDependant employeeDependant)
        {
            if (id != employeeDependant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDependant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDependantExists(employeeDependant.Id))
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
            return View(employeeDependant);
        }

        // GET: EmployeeDependants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeDependant = await _context.EmployeeDependants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeDependant == null)
            {
                return NotFound();
            }

            return View(employeeDependant);
        }

        // POST: EmployeeDependants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employeeDependant = await _context.EmployeeDependants.FindAsync(id);
            _context.EmployeeDependants.Remove(employeeDependant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDependantExists(Guid id)
        {
            return _context.EmployeeDependants.Any(e => e.Id == id);
        }
    }
}
