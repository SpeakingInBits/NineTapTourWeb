using Microsoft.AspNetCore.Identity;

namespace NineTapTourWeb.Models;

public static class IdentityHelper
{
    /* Role Name */
    public const string AdminRole = "Admin";

    internal static void ConfigureIdentityOptions(IdentityOptions options)
    {
        // Password settings
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;

        // SignIn settings
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = false;
    }

    internal static async Task CreateDefaultAdmin(IServiceProvider service)
    {
        using (var scope = service.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            string adminEmail = "admin@admin.com";
            string adminPassword = "TheAdminIsHere";

            // Check if the admin user already exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                // Create the admin user
                adminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = adminEmail
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Ensure the admin role exists
                    if (!await roleManager.RoleExistsAsync(AdminRole))
                    {
                        await roleManager.CreateAsync(new ApplicationRole(AdminRole));
                    }

                    // Add the user to the admin role
                    await userManager.AddToRoleAsync(adminUser, AdminRole);
                }
            }
        }
    }

    internal static async Task SeedRoles(IServiceProvider service)
    {
        using (var scope = service.CreateScope())
        {
            RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            string[] roleNames = { AdminRole };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the roles and seed them to the database
                    roleResult = await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }
        }
    }
}

