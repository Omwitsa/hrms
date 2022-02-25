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
    public class WorkFlowDocumentsController : Controller
    {
        private readonly HrDbContext _context;

        public WorkFlowDocumentsController(HrDbContext context)
        {
            _context = context;
        }

        // GET: WorkFlowDocuments
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkFlowDocuments.ToListAsync());
        }

        // GET: WorkFlowDocuments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowDocument = await _context.WorkFlowDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowDocument == null)
            {
                return NotFound();
            }

            return View(workFlowDocument);
        }

        // GET: WorkFlowDocuments/Create
        public IActionResult Create()
        {
            ViewBag.success = true;
            return View();
        }

        // POST: WorkFlowDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,No,Type,Description,UserRef,LatestApprover,FinalStatus,Personnel,CreatedDate")] WorkFlowDocument workFlowDocument)
        {
            //if (_context.WorkFlowDocuments.Any(d => d.Type.ToUpper().Equals(workFlowDocument.Type.ToUpper())))
            //{
            //    ViewBag.success = false;
            //    TempData["message"] = "Sorry, Documennt already exist";
            //    return View(workFlowDocument);
            //}

            if (ModelState.IsValid)
            {
                workFlowDocument.Id = Guid.NewGuid();
                _context.Add(workFlowDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workFlowDocument);
        }

        // GET: WorkFlowDocuments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowDocument = await _context.WorkFlowDocuments.FindAsync(id);
            if (workFlowDocument == null)
            {
                return NotFound();
            }
            return View(workFlowDocument);
        }

        // POST: WorkFlowDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,No,Type,Description,UserRef,LatestApprover,FinalStatus,Personnel,CreatedDate")] WorkFlowDocument workFlowDocument)
        {
            if (id != workFlowDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workFlowDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkFlowDocumentExists(workFlowDocument.Id))
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
            return View(workFlowDocument);
        }

        // GET: WorkFlowDocuments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowDocument = await _context.WorkFlowDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowDocument == null)
            {
                return NotFound();
            }

            return View(workFlowDocument);
        }

        // POST: WorkFlowDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workFlowDocument = await _context.WorkFlowDocuments.FindAsync(id);
            _context.WorkFlowDocuments.Remove(workFlowDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkFlowDocumentExists(Guid id)
        {
            return _context.WorkFlowDocuments.Any(e => e.Id == id);
        }
    }
}
