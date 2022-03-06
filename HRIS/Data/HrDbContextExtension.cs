using HRIS.Constants;
using HRIS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;

namespace HRIS.Data
{
    public static class HrDbContextExtension
    {
        public static void EnsureDatabaseSeeded(this HrDbContext context)
        {
            if (!context.HRSetup.Any())
            {
                context.HRSetup.Add(new HRSetup
                {
                    RetirementAge = 65
                });
            } 
            if (!context.Branches.Any())
            {
                context.Branches.Add(new Branch
                {
                    Name = "All"
                });
            }
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var adminRole = new IdentityRole(Roles.Admin.ToString());
                adminRole.NormalizedName = Roles.Admin.ToString().ToUpper();
                roleStore.CreateAsync(adminRole);
                var staffRole = new IdentityRole(Roles.Staff.ToString());
                staffRole.NormalizedName = Roles.Staff.ToString().ToUpper();
                roleStore.CreateAsync(staffRole);
                var hrRole = new IdentityRole(Roles.Hr.ToString());
                hrRole.NormalizedName = Roles.Hr.ToString().ToUpper();
                roleStore.CreateAsync(hrRole);
            }
            if (!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    FirstName = "Wilson",
                    LastName = "Omwitsa",
                    Email = "wilsonomwitsa98@gmail.com",
                    NormalizedEmail = "WILSONOMWITSA98@GMAIL.COM",
                    UserName = "wilsonomwitsa98@gmail.com",
                    NormalizedUserName = "WILSONOMWITSA98@GMAIL.COM",
                    PhoneNumber = "+254715507260",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "A123.123z");
                user.PasswordHash = hashed;
                var userManager = new UserStore<ApplicationUser>(context);
                var result = userManager.CreateAsync(user);
                userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

            context.SaveChanges();
        }
    }
}
