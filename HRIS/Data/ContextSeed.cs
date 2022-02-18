using HRIS.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ////Seed Roles
            //await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Hr.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Employee.ToString()));
        }
    }
}
