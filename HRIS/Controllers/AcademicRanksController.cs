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
    public class AcademicRanksController : Controller
    {
        private readonly HrDbContext _context;

        public AcademicRanksController(HrDbContext context)
        {
            _context = context;
        }

        // GET: AcademicRanks
        public async Task<IActionResult> Index()
        {
            return View(await _context.AcademicRanks.ToListAsync());
        }

        // GET: AcademicRanks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicRank = await _context.AcademicRanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicRank == null)
            {
                return NotFound();
            }

            return View(academicRank);
        }

        // GET: AcademicRanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicRanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Notes,Closed")] AcademicRank academicRank)
        {
            if (ModelState.IsValid)
            {
                academicRank.Id = Guid.NewGuid();
                _context.Add(academicRank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicRank);
        }

        // GET: AcademicRanks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicRank = await _context.AcademicRanks.FindAsync(id);
            if (academicRank == null)
            {
                return NotFound();
            }
            return View(academicRank);
        }

        // POST: AcademicRanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Notes,Closed")] AcademicRank academicRank)
        {
            if (id != academicRank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicRank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicRankExists(academicRank.Id))
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
            return View(academicRank);
        }

        // GET: AcademicRanks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicRank = await _context.AcademicRanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (academicRank == null)
            {
                return NotFound();
            }

            return View(academicRank);
        }

        // POST: AcademicRanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var academicRank = await _context.AcademicRanks.FindAsync(id);
            _context.AcademicRanks.Remove(academicRank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicRankExists(Guid id)
        {
            return _context.AcademicRanks.Any(e => e.Id == id);
        }
    }
}
