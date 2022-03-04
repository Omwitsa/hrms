using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;
using HRIS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;
using HRIS.IProviders;

namespace HRIS.Controllers
{
    public class WorkFlowApproversController : Controller
    {
        private readonly INotyfService _notyf;
        private IHrProvider _hrProvider;
        private readonly HrDbContext _context;

        public WorkFlowApproversController(HrDbContext context, IHrProvider hrProvider, INotyfService notyf)
        {
            _context = context;
            _hrProvider = hrProvider;
            _notyf = notyf;
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
            SetInitialValues();
            return View();
        }

        private void SetInitialValues()
        {
            ViewBag.success = true;
            var designations = _context.Designations.Where(d => !d.Closed)
               .Select(d => new Designation
               {
                   Name = d.Name
               }).ToList();
            ViewBag.designations = new SelectList(designations, "Name", "Name");

            var employees = _context.Employees.Where(d => !d.Terminated)
              .Select(d => new Employee
              {
                  EmployeeNo = d.EmployeeNo,
                  Name = d.Name
              }).ToList();
            ViewBag.employees = new SelectList(employees, "EmployeeNo", "Name");
        }

        // POST: WorkFlowApprovers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Closed,Notes,Personnel,CreatedDate,ModifiedDate")] WorkFlowApprover workFlowApprover)
        {
            SetInitialValues();
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

        [HttpPost]
        public JsonResult SaveWorkFlowApprover([FromBody] WorkFlowApprover approver)
        {
            var results = _hrProvider.SaveWorkFlowApprover(approver);
            if (!results.Success)
            {
                _notyf.Error(results.Message);
                return Json(results);
            }

            _notyf.Success(results.Message);
            return Json(results);
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
