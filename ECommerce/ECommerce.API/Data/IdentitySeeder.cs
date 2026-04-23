using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.API.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // 1) Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "Admin", Description = "Admin role" });

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "User", Description = "Normal user role" });

            // 2) Admin user
            var adminEmail = "admin@ecommerce.com";
            var adminPassword = "Admin123abc";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin is null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User"
                };

                var created = await userManager.CreateAsync(admin, adminPassword);
                if (created.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");

            }

            if (admin is not null && !await userManager.IsInRoleAsync(admin, "Admin"))
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
} 
