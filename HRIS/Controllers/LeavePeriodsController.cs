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
    public class LeavePeriodsController : Controller
    {
        private readonly HrDbContext _context;

        public LeavePeriodsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: LeavePeriods
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeavePeriods.ToListAsync());
        }

        // GET: LeavePeriods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.LeavePeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leavePeriod == null)
            {
                return NotFound();
            }

            return View(leavePeriod);
        }

        // GET: LeavePeriods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeavePeriods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,Notes")] LeavePeriod leavePeriod)
        {
            if (ModelState.IsValid)
            {
                leavePeriod.Id = Guid.NewGuid();
                _context.Add(leavePeriod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leavePeriod);
        }

        // GET: LeavePeriods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.LeavePeriods.FindAsync(id);
            if (leavePeriod == null)
            {
                return NotFound();
            }
            return View(leavePeriod);
        }

        // POST: LeavePeriods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,StartDate,EndDate,Notes")] LeavePeriod leavePeriod)
        {
            if (id != leavePeriod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leavePeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeavePeriodExists(leavePeriod.Id))
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
            return View(leavePeriod);
        }

        // GET: LeavePeriods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leavePeriod = await _context.LeavePeriods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leavePeriod == null)
            {
                return NotFound();
            }

            return View(leavePeriod);
        }

        // POST: LeavePeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leavePeriod = await _context.LeavePeriods.FindAsync(id);
            _context.LeavePeriods.Remove(leavePeriod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeavePeriodExists(Guid id)
        {
            return _context.LeavePeriods.Any(e => e.Id == id);
        }
    }
}
