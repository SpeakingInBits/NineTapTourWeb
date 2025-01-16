using Microsoft.AspNetCore.Identity;

namespace NineTapTourWeb.Models;

/// <summary>
/// Represents a user in the application and allows
/// customizing the default user properties.
/// </summary>
public class ApplicationUser : IdentityUser<int>
{
}

/// <summary>
/// Represents a role in the application and allows
/// customizing the default role properties.
/// </summary>
public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole(string roleName) : base(roleName)
    {
    }

    public ApplicationRole()
    {
    }
}