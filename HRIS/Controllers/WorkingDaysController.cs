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
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HRIS.Controllers
{
    public class WorkingDaysController : Controller
    {
        private readonly HrDbContext _context;
        private readonly INotyfService _notyf;

        public WorkingDaysController(HrDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: WorkingDays
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkingDays.ToListAsync());
        }

        // GET: WorkingDays/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingDay = await _context.WorkingDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workingDay == null)
            {
                return NotFound();
            }

            return View(workingDay);
        }

        // GET: WorkingDays/Create
        public IActionResult Create()
        {
            SetInitialVaues();
            return View();
        }

        private void SetInitialVaues()
        {
            ViewBag.success = true;
            ViewBag.weekDays = new SelectList(ArrValues.WeekDays);
            ViewBag.dayTypes = new SelectList(ArrValues.DayTypes);
            var branches = _context.Branches.Where(d => !d.Closed)
               .Select(d => new Branch
               {
                   Name = d.Name
               }).ToList();
            ViewBag.branches = new SelectList(branches, "Name", "Name");
        }

        // POST: WorkingDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Branch,Notes")] WorkingDay workingDay)
        {
            SetInitialVaues();
            string message = "";
            var dayExist = _context.WorkingDays.Any(d => d.Name.ToUpper().Equals(workingDay.Name.ToUpper())
            && d.Branch.ToUpper().Equals(workingDay.Branch.ToUpper()));
            if (dayExist)
            {
                ViewBag.success = false;
                message = "Sorry, Working day already exist";
                TempData["message"] = message;
                _notyf.Error(message);
                return View(workingDay);
            }

            if (ModelState.IsValid)
            {
                workingDay.Id = Guid.NewGuid();
                _context.Add(workingDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workingDay);
        }

        // GET: WorkingDays/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingDay = await _context.WorkingDays.FindAsync(id);
            if (workingDay == null)
            {
                return NotFound();
            }
            return View(workingDay);
        }

        // POST: WorkingDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,Branch,Notes")] WorkingDay workingDay)
        {
            if (id != workingDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workingDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkingDayExists(workingDay.Id))
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
            return View(workingDay);
        }

        // GET: WorkingDays/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingDay = await _context.WorkingDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workingDay == null)
            {
                return NotFound();
            }

            return View(workingDay);
        }

        // POST: WorkingDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workingDay = await _context.WorkingDays.FindAsync(id);
            _context.WorkingDays.Remove(workingDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkingDayExists(Guid id)
        {
            return _context.WorkingDays.Any(e => e.Id == id);
        }
    }
}
