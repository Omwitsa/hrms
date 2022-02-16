using HRIS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Data
{
    public class HrDbContext : IdentityDbContext
    {
        public HrDbContext(DbContextOptions<HrDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public virtual DbSet<AcademicRank> AcademicRanks { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<EmpCategory> EmpCategories { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDependant> EmployeeDependants { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<HRSetup> HRSetup { get; set; }
        public virtual DbSet<JobCategory> JobCategories { get; set; }
        public virtual DbSet<LeaveApplication> LeaveApplications { get; set; }
        public virtual DbSet<LeaveEntilement> LeaveEntilements { get; set; }
        public virtual DbSet<LeaveGroup> LeaveGroups { get; set; }
        public virtual DbSet<LeavePeriod> LeavePeriods { get; set; }
        public virtual DbSet<LeaveRule> LeaveRules { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<WorkingDay> WorkingDays { get; set; }
    }
}
