﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;
using HRIS.Models;

namespace HRIS.Controllers
{
    public class WorkFlowApproversController : Controller
    {
        private readonly HrDbContext _context;

        public WorkFlowApproversController(HrDbContext context)
        {
            _context = context;
        }

        // GET: WorkFlowApprovers
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkFlowApprovers.ToListAsync());
        }

        // GET: WorkFlowApprovers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowApprover = await _context.WorkFlowApprovers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowApprover == null)
            {
                return NotFound();
            }

            return View(workFlowApprover);
        }

        // GET: WorkFlowApprovers/Create
        public IActionResult Create()
        {
            ViewBag.success = true;
            return View();
        }

        // POST: WorkFlowApprovers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Closed,Notes,Personnel,CreatedDate,ModifiedDate")] WorkFlowApprover workFlowApprover)
        {
            //if (_context.WorkFlowApprovers.Any(d => d.Title.ToUpper().Equals(workFlowApprover.Title.ToUpper())))
            //{
            //    ViewBag.success = false;
            //    TempData["message"] = "Sorry, Approver already exist";
            //    return View(workFlowApprover);
            //}

            if (ModelState.IsValid)
            {
                workFlowApprover.Id = Guid.NewGuid();
                _context.Add(workFlowApprover);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workFlowApprover);
        }

        // GET: WorkFlowApprovers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowApprover = await _context.WorkFlowApprovers.FindAsync(id);
            if (workFlowApprover == null)
            {
                return NotFound();
            }
            return View(workFlowApprover);
        }

        // POST: WorkFlowApprovers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Closed,Notes,Personnel,CreatedDate,ModifiedDate")] WorkFlowApprover workFlowApprover)
        {
            if (id != workFlowApprover.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workFlowApprover);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkFlowApproverExists(workFlowApprover.Id))
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
            return View(workFlowApprover);
        }

        // GET: WorkFlowApprovers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowApprover = await _context.WorkFlowApprovers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowApprover == null)
            {
                return NotFound();
            }

            return View(workFlowApprover);
        }

        // POST: WorkFlowApprovers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workFlowApprover = await _context.WorkFlowApprovers.FindAsync(id);
            _context.WorkFlowApprovers.Remove(workFlowApprover);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkFlowApproverExists(Guid id)
        {
            return _context.WorkFlowApprovers.Any(e => e.Id == id);
        }
    }
}