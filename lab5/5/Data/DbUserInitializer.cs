using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace _5.Data
{
    public static class DbUserInitializer
    {
        public static async Task Initialize(HttpContext context)
        {
            UserManager<ApplicationUser> userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = context.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
            string adminEmail = "admin@gmail.com";
            string adminName = "admin@gmail.com";

            string password = "123456";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminName,
                    RegistrationDate = DateTime.Now
                };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
