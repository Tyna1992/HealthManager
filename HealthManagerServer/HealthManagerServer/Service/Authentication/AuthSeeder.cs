
using Microsoft.AspNetCore.Identity;

namespace HealthManagerServer.Service.Authentication;
public class AuthSeeder
{
    private RoleManager<IdentityRole> _roleManager;
    private UserManager<ApplicationUser> userManager;

    public AuthSeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        this._roleManager = roleManager;
        this.userManager = userManager;
    }

    public void AddRoles()
    {
        var tAdmin = CreateAdminRole(_roleManager);
        tAdmin.Wait();

        var tUser = CreateUserRole(_roleManager);
        tUser.Wait();
    }
    public void AddAdmin()
    {
        var tAdmin = CreateAdminIfNotExists();
        tAdmin.Wait();
    }

    private async Task CreateAdminIfNotExists()
    {
        var adminInDb = await userManager.FindByEmailAsync("admin@admin.com");
        if (adminInDb == null)
        {
            var admin = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", Weight = 0, Gender = "" };
            var adminCreated = await userManager.CreateAsync(admin, "admin");

            if (adminCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }

    private async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin")); 
    }

    async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("User")); 
    }
}