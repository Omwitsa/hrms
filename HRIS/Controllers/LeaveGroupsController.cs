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
    public class LeaveGroupsController : Controller
    {
        private readonly HrDbContext _context;

        public LeaveGroupsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: LeaveGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeaveGroups.ToListAsync());
        }

        // GET: LeaveGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveGroup = await _context.LeaveGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveGroup == null)
            {
                return NotFound();
            }

            return View(leaveGroup);
        }

        // GET: LeaveGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,Closed")] LeaveGroup leaveGroup)
        {
            if (ModelState.IsValid)
            {
                leaveGroup.Id = Guid.NewGuid();
                _context.Add(leaveGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveGroup);
        }

        // GET: LeaveGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveGroup = await _context.LeaveGroups.FindAsync(id);
            if (leaveGroup == null)
            {
                return NotFound();
            }
            return View(leaveGroup);
        }

        // POST: LeaveGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Notes,Closed")] LeaveGroup leaveGroup)
        {
            if (id != leaveGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveGroupExists(leaveGroup.Id))
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
            return View(leaveGroup);
        }

        // GET: LeaveGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveGroup = await _context.LeaveGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveGroup == null)
            {
                return NotFound();
            }

            return View(leaveGroup);
        }

        // POST: LeaveGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leaveGroup = await _context.LeaveGroups.FindAsync(id);
            _context.LeaveGroups.Remove(leaveGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveGroupExists(Guid id)
        {
            return _context.LeaveGroups.Any(e => e.Id == id);
        }
    }
}
