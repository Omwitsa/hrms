using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Employee No.")]
        public string EmployeeNo { get; set; }
    }
}
