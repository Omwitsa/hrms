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
    public class EmployeesController : Controller
    {
        private readonly HrDbContext _context;

        public EmployeesController(HrDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeNo == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.success = true;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNo,Name,IdNo,DateOfBirth,HiredDate,Location,LeaveGroup,Gender,Department,MaritalStatus,Spouse,PIN,NSSF,NHIF,Division,Race,Religion,Disability,Country,County,City,Address,PostalCode,JobCategory,EmploymentCategory,Notes,PersonalEmail,WorkEmail,Cell,TelNo,Web,Terminated,TerminationDate,TerminationType,TerminationNotes,Supervisor,Language,Title,Personnel,CreatedDate,ModifiedDate")] Employee employee)
        {
            if (_context.Employees.Any(d => d.EmployeeNo.ToUpper().Equals(employee.EmployeeNo.ToUpper())))
            {
                ViewBag.success = false;
                TempData["message"] = "Sorry, Employee No. already exist";
                return View(employee);
            }

            if (_context.Employees.Any(d => d.Name.ToUpper().Equals(employee.Name.ToUpper())))
            {
                ViewBag.success = false;
                TempData["message"] = "Sorry, Employee Names already exist";
                return View(employee);
            }

            if (_context.Employees.Any(d => d.IdNo.ToUpper().Equals(employee.IdNo.ToUpper())))
            {
                ViewBag.success = false;
                TempData["message"] = "Sorry, Employee Id No. already exist";
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeNo,Name,IdNo,DateOfBirth,HiredDate,Location,LeaveGroup,Gender,Department,MaritalStatus,Spouse,PIN,NSSF,NHIF,Division,Race,Religion,Disability,Country,County,City,Address,PostalCode,JobCategory,EmploymentCategory,Notes,PersonalEmail,WorkEmail,Cell,TelNo,Web,Terminated,TerminationDate,TerminationType,TerminationNotes,Supervisor,Language,Title,Personnel,CreatedDate,ModifiedDate")] Employee employee)
        {
            if (id != employee.EmployeeNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeNo))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeNo == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeeNo == id);
        }
    }
}
