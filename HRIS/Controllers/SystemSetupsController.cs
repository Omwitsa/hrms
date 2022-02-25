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
    public class SystemSetupsController : Controller
    {
        private readonly HrDbContext _context;

        public SystemSetupsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: SystemSetups
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemSetup.ToListAsync());
        }

        // GET: SystemSetups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSetup = await _context.SystemSetup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSetup == null)
            {
                return NotFound();
            }

            return View(systemSetup);
        }

        // GET: SystemSetups/Create
        public IActionResult Create()
        {
            ViewBag.success = true;
            return View();
        }

        // POST: SystemSetups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgName,OrgInitial,LogoUrl,PrimaryColor,SecondaryColor")] SystemSetup systemSetup)
        {
            //if (string.IsNullOrEmpty(systemSetup.OrgName))
            //{
            //    ViewBag.success = false;
            //    TempData["message"] = "Sorry, Kindly provide organization name";
            //    return View(systemSetup);
            //}

            if (ModelState.IsValid)
            {
                systemSetup.Id = Guid.NewGuid();
                _context.Add(systemSetup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemSetup);
        }

        // GET: SystemSetups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSetup = await _context.SystemSetup.FindAsync(id);
            if (systemSetup == null)
            {
                return NotFound();
            }
            return View(systemSetup);
        }

        // POST: SystemSetups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrgName,OrgInitial,LogoUrl,PrimaryColor,SecondaryColor")] SystemSetup systemSetup)
        {
            if (id != systemSetup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemSetup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemSetupExists(systemSetup.Id))
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
            return View(systemSetup);
        }

        // GET: SystemSetups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSetup = await _context.SystemSetup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSetup == null)
            {
                return NotFound();
            }

            return View(systemSetup);
        }

        // POST: SystemSetups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var systemSetup = await _context.SystemSetup.FindAsync(id);
            _context.SystemSetup.Remove(systemSetup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemSetupExists(Guid id)
        {
            return _context.SystemSetup.Any(e => e.Id == id);
        }
    }
}
