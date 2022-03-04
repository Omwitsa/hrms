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
    public class EmployeesController : Controller
    {
        private readonly HrDbContext _context;
        private readonly INotyfService _notyf;

        public EmployeesController(HrDbContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
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
            //new SelectList(ArrValues.Titles, "", "");
            SetInitialValues();
            return View();
        }

        private void SetInitialValues()
        {
            ViewBag.success = true;
            ViewBag.titles = new SelectList(ArrValues.Titles);
            ViewBag.maritalStatuses = new SelectList(ArrValues.MaritalStatuses);
            var genders = ArrValues.Genders.Where(g => g != "All");
            ViewBag.genders = new SelectList(genders);
            var branches = _context.Branches.Where(b => !b.Closed)
                .Select(b => new Branch
                {
                    Name = b.Name
                }).ToList();
            ViewBag.branches = new SelectList(branches, "Name", "Name");
            var departments = _context.Departments.Where(d => !d.Closed)
                .Select(d => new Department
                {
                    Name = d.Name
                }).ToList();
            ViewBag.departments = new SelectList(departments, "Name", "Name");
            var empCategories = _context.EmpCategories.Where(c => !c.Closed)
                .Select(c => new EmpCategory
                {
                    Name = c.Name
                }).ToList();
            ViewBag.empCategories = new SelectList(empCategories, "Name", "Name");
            var jobCategories = _context.JobCategories.Where(c => !c.Closed)
                .Select(c => new JobCategory
                {
                    Name = c.Name
                }).ToList();
            ViewBag.jobCategories = new SelectList(jobCategories, "Name", "Name");
            var leaveGroups = _context.LeaveGroups.Where(g => !g.Closed)
                .Select(g => new LeaveGroup
                {
                    Name = g.Name
                }).ToList();
            ViewBag.leaveGroups = new SelectList(leaveGroups, "Name", "Name");
            var divisions = _context.Divisions.Where(d => !d.Closed)
                .Select(d => new Division
                {
                    Name = d.Name
                }).ToList();
            ViewBag.divisions = new SelectList(divisions, "Name", "Name");
            var races = _context.Races.Where(r => !r.Closed)
                .Select(r => new Race
                {
                    Name = r.Name
                }).ToList();
            ViewBag.races = new SelectList(races, "Name", "Name");
            var countries = _context.Countries.Where(c => !c.Closed)
                .Select(c => new Country
                {
                    Name = c.Name
                }).ToList();
            ViewBag.countries = new SelectList(countries, "Name", "Name");
            var counties = _context.Counties.Where(c => !c.Closed)
                .Select(c => new County
                {
                    Name = c.Name
                }).ToList();
            ViewBag.counties = new SelectList(counties, "Name", "Name");
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNo,Name,IdNo,DateOfBirth,HiredDate,Location,LeaveGroup,Gender,Department,MaritalStatus,Spouse,PIN,NSSF,NHIF,Division,Race,Religion,Disability,Country,County,City,Address,PostalCode,JobCategory,EmploymentCategory,Notes,PersonalEmail,WorkEmail,Cell,TelNo,Web,Terminated,TerminationDate,TerminationType,TerminationNotes,Supervisor,Language,Title,Personnel,CreatedDate,ModifiedDate")] Employee employee)
        {
            SetInitialValues();
            string message = "";
            if (string.IsNullOrEmpty(employee.EmployeeNo))
            {
                ViewBag.success = false;
                message = "Sorry, Kindly provide employee No.";
                TempData["message"] = message;
                _notyf.Error(message);
                View(employee);
            }

            if (string.IsNullOrEmpty(employee.Name))
            {
                ViewBag.success = false;
                message = "Sorry, Kindly provide employee Names";
                TempData["message"] = message;
                _notyf.Error(message);
                View(employee);
            }

            if (string.IsNullOrEmpty(employee.IdNo))
            {
                ViewBag.success = false;
                message = "Sorry, Kindly provide Id No.";
                TempData["message"] = message;
                _notyf.Error(message);
                View(employee);
            }

            if (_context.Employees.Any(d => d.EmployeeNo.ToUpper().Equals(employee.EmployeeNo.ToUpper())))
            {
                ViewBag.success = false;
                message = "Sorry, Employee No. already exist";
                TempData["message"] = message;
                _notyf.Error(message);
                View(employee);
            }

            if (_context.Employees.Any(d => d.Name.ToUpper().Equals(employee.Name.ToUpper())))
            {
                ViewBag.success = false;
                message = "Sorry, Employee Names already exist";
                TempData["message"] = message;
                _notyf.Error(message);
                View(employee);
            }

            if (_context.Employees.Any(d => d.IdNo.ToUpper().Equals(employee.IdNo.ToUpper())))
            {
                ViewBag.success = false;
                message = "Sorry, Employee Id No. already exist";
                TempData["message"] = message;
                _notyf.Error(message);
                View(employee);
            }

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                _notyf.Success("Employee saved successfuly");
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

            SetInitialValues();
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

            SetInitialValues();
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
