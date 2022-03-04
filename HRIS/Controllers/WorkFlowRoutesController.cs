using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;
using HRIS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRIS.Constants;
using HRIS.IProviders;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HRIS.Controllers
{
    public class WorkFlowRoutesController : Controller
    {
        private readonly INotyfService _notyf;
        private IHrProvider _hrProvider;
        private readonly HrDbContext _context;

        public WorkFlowRoutesController(HrDbContext context, IHrProvider hrProvider, INotyfService notyf)
        {
            _context = context;
            _hrProvider = hrProvider;
            _notyf = notyf;
        }

        // GET: WorkFlowRoutes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkFlowRoutes.ToListAsync());
        }

        // GET: WorkFlowRoutes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowRoute = await _context.WorkFlowRoutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowRoute == null)
            {
                return NotFound();
            }

            return View(workFlowRoute);
        }

        // GET: WorkFlowRoutes/Create
        public IActionResult Create()
        {
            SetInitialValues();
            return View();
        }

        private void SetInitialValues()
        {
            ViewBag.success = true;
            ViewBag.workflowDocs = new SelectList(ArrValues.WorkflowDocs);
            var approvers = _context.WorkFlowApprovers.Where(d => !d.Closed)
               .Select(d => new WorkFlowApprover
               {
                   Title = d.Title,
               }).ToList();
            ViewBag.approvers = new SelectList(approvers, "Title", "Title");
        }

        [HttpPost]
        public JsonResult SaveWorkFlowRoute([FromBody] WorkFlowRoute route)
        {
            var results = _hrProvider.SaveWorkFlowRoute(route);
            if (!results.Success)
            {
                _notyf.Error(results.Message);
                return Json(results);
            }
                
            _notyf.Success(results.Message);
            return Json(results);
        }

        // POST: WorkFlowRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Document,Closed,Notes,Personnel,CreatedDate,ModifiedDate")] WorkFlowRoute workFlowRoute)
        {
            SetInitialValues();

            if (_context.WorkFlowRoutes.Any(d => d.Document.ToUpper().Equals(workFlowRoute.Document.ToUpper())))
            {
                ViewBag.success = false;
                TempData["message"] = "Sorry, Routes already exist";
                return View(workFlowRoute);
            }

            if (ModelState.IsValid)
            {
                workFlowRoute.Id = Guid.NewGuid();
                _context.Add(workFlowRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workFlowRoute);
        }

        // GET: WorkFlowRoutes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowRoute = await _context.WorkFlowRoutes.FindAsync(id);
            if (workFlowRoute == null)
            {
                return NotFound();
            }
            return View(workFlowRoute);
        }

        // POST: WorkFlowRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Document,Closed,Notes,Personnel,CreatedDate,ModifiedDate")] WorkFlowRoute workFlowRoute)
        {
            if (id != workFlowRoute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workFlowRoute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkFlowRouteExists(workFlowRoute.Id))
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
            return View(workFlowRoute);
        }

        // GET: WorkFlowRoutes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workFlowRoute = await _context.WorkFlowRoutes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workFlowRoute == null)
            {
                return NotFound();
            }

            return View(workFlowRoute);
        }

        // POST: WorkFlowRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var workFlowRoute = await _context.WorkFlowRoutes.FindAsync(id);
            if(workFlowRoute != null)
            {
                var details = await _context.WorkFlowRouteDetails.Where(d => d.WorkFlowRouteId == workFlowRoute.Id).ToListAsync();
                if (details.Any())
                    _context.WorkFlowRouteDetails.RemoveRange(details);
            }
            _context.WorkFlowRoutes.Remove(workFlowRoute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkFlowRouteExists(Guid id)
        {
            return _context.WorkFlowRoutes.Any(e => e.Id == id);
        }
    }
}
