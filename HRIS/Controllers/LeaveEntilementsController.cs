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
    public class LeaveEntilementsController : Controller
    {
        private readonly HrDbContext _context;

        public LeaveEntilementsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: LeaveEntilements
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeaveEntilements.ToListAsync());
        }

        // GET: LeaveEntilements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveEntilement = await _context.LeaveEntilements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveEntilement == null)
            {
                return NotFound();
            }

            return View(leaveEntilement);
        }

        // GET: LeaveEntilements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveEntilements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Period,EmpNo,LeaveType,Type,Days,Notes,Personnel,CreatedDate,ModifiedDate")] LeaveEntilement leaveEntilement)
        {
            if (ModelState.IsValid)
            {
                leaveEntilement.Id = Guid.NewGuid();
                _context.Add(leaveEntilement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveEntilement);
        }

        // GET: LeaveEntilements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveEntilement = await _context.LeaveEntilements.FindAsync(id);
            if (leaveEntilement == null)
            {
                return NotFound();
            }
            return View(leaveEntilement);
        }

        // POST: LeaveEntilements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Period,EmpNo,LeaveType,Type,Days,Notes,Personnel,CreatedDate,ModifiedDate")] LeaveEntilement leaveEntilement)
        {
            if (id != leaveEntilement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveEntilement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveEntilementExists(leaveEntilement.Id))
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
            return View(leaveEntilement);
        }

        // GET: LeaveEntilements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveEntilement = await _context.LeaveEntilements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveEntilement == null)
            {
                return NotFound();
            }

            return View(leaveEntilement);
        }

        // POST: LeaveEntilements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leaveEntilement = await _context.LeaveEntilements.FindAsync(id);
            _context.LeaveEntilements.Remove(leaveEntilement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveEntilementExists(Guid id)
        {
            return _context.LeaveEntilements.Any(e => e.Id == id);
        }
    }
}
