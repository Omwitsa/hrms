using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;
using HRIS.Models;
using HRIS.Constants;

namespace HRIS.Controllers
{
    public class HolidaysController : Controller
    {
        private readonly HrDbContext _context;

        public HolidaysController(HrDbContext context)
        {
            _context = context;
        }

        // GET: Holidays
        public async Task<IActionResult> Index()
        {
            return View(await _context.Holidays.ToListAsync());
        }

        // GET: Holidays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (holiday == null)
            {
                return NotFound();
            }

            return View(holiday);
        }

        // GET: Holidays/Create
        public IActionResult Create()
        {
            ViewBag.success = true;
            ViewBag.dayTypes = new SelectList(ArrValues.DayTypes);
            var periods = _context.LeavePeriods.Where(d => d.StartDate <= DateTime.Today && d.EndDate >= DateTime.Today)
               .Select(d => new LeavePeriod
               {
                   Name = d.Name
               }).ToList();
            ViewBag.periods = new SelectList(periods, "Name", "Name");
            var branches = _context.Branches.Where(d => !d.Closed)
               .Select(d => new Branch
               {
                   Name = d.Name
               }).ToList();
            ViewBag.branches = new SelectList(branches, "Name", "Name");
            return View();
        }

        // POST: Holidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Period,Type,Branch,Recur,Notes,Personnel,CreatedDate,ModifiedDate")] Holiday holiday)
        {
            holiday.Branch = holiday?.Branch ?? "";
            var holidayExist = _context.Holidays.Any(d => d.Name.ToUpper().Equals(holiday.Name.ToUpper())
            && d.Period.ToUpper().Equals(holiday.Period.ToUpper()) && d.Branch.ToUpper().Equals(holiday.Branch.ToUpper()));
            if (holidayExist)
            {
                ViewBag.success = false;
                TempData["message"] = "Sorry, Holiday already exist";
                return View(holiday);
            }

            if (ModelState.IsValid)
            {
                holiday.Id = Guid.NewGuid();
                _context.Add(holiday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(holiday);
        }

        // GET: Holidays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }
            return View(holiday);
        }

        // POST: Holidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Date,Period,Type,Branch,Recur,Notes,Personnel,CreatedDate,ModifiedDate")] Holiday holiday)
        {
            if (id != holiday.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(holiday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HolidayExists(holiday.Id))
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
            return View(holiday);
        }

        // GET: Holidays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holidays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (holiday == null)
            {
                return NotFound();
            }

            return View(holiday);
        }

        // POST: Holidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HolidayExists(Guid id)
        {
            return _context.Holidays.Any(e => e.Id == id);
        }
    }
}
