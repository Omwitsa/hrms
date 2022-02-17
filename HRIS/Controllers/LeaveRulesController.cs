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
    public class LeaveRulesController : Controller
    {
        private readonly HrDbContext _context;

        public LeaveRulesController(HrDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRules
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeaveRules.ToListAsync());
        }

        // GET: LeaveRules/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRule = await _context.LeaveRules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRule == null)
            {
                return NotFound();
            }

            return View(leaveRule);
        }

        // GET: LeaveRules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LeaveGroup,LeaveType,LeaveDays,Gender,StartDate,EndDate,Notes,Personnel,CreatedDate,ModifiedDate")] LeaveRule leaveRule)
        {
            if (ModelState.IsValid)
            {
                leaveRule.Id = Guid.NewGuid();
                _context.Add(leaveRule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRule);
        }

        // GET: LeaveRules/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRule = await _context.LeaveRules.FindAsync(id);
            if (leaveRule == null)
            {
                return NotFound();
            }
            return View(leaveRule);
        }

        // POST: LeaveRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,LeaveGroup,LeaveType,LeaveDays,Gender,StartDate,EndDate,Notes,Personnel,CreatedDate,ModifiedDate")] LeaveRule leaveRule)
        {
            if (id != leaveRule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRuleExists(leaveRule.Id))
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
            return View(leaveRule);
        }

        // GET: LeaveRules/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRule = await _context.LeaveRules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRule == null)
            {
                return NotFound();
            }

            return View(leaveRule);
        }

        // POST: LeaveRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leaveRule = await _context.LeaveRules.FindAsync(id);
            _context.LeaveRules.Remove(leaveRule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRuleExists(Guid id)
        {
            return _context.LeaveRules.Any(e => e.Id == id);
        }
    }
}
