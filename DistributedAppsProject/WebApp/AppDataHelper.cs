using System.Reflection;
using App.DAL;
using App.DAL.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class WebAppHelperMethods
{
    public static void SetUpAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
    {
        using var serviceScope = app
            .ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope
            .ServiceProvider
            .GetService<ApplicationDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Db context wasn't found!");
        }

        if (config.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }
        
        if (config.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }
        
        if (config.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            if (userManager == null || roleManager == null) throw new NullReferenceException("userManager or roleManager cannot be null!");
            var roles = new string[]
            {
                "admin",
                "user"
            };

            foreach (var roleInfo in roles)
            {
                var role = roleManager.FindByNameAsync(roleInfo).Result;
                if (role != null) continue;
                
                var identityResult = roleManager.CreateAsync(new AppRole{Name = roleInfo, NormalizedName = roleInfo.ToUpper()}).Result;
                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Role creation failed");
                }
            }

            var users = new (string username, string password, string roles)[]
            {
                ("admin@itcollege.ee", "Kala.maja1!", "user, admin"),
                ("glenga@itcollege.ee", "Kala.maja1!", "user, admin"),
                ("user@itcollege.ee", "Kala.maja1!", "user"),
                ("newuser@itcollege.ee", "Kala.maja1!",  "")
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.username).Result;
                if (user != null) continue;

                user = new AppUser()
                {
                    Email = userInfo.username,
                    UserName = userInfo.username,
                    EmailConfirmed = true,
                };
                var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Cannot create user!");
                }

                if (!string.IsNullOrWhiteSpace(userInfo.roles))
                {
                    var identityResultRole = userManager.AddToRolesAsync(user, 
                        userInfo.roles.Split(",").Select(r => r.Trim())
                    ).Result;
                }
            }
        }
    }

    public static List<string> GetAdminAreaControllersNames()
    {
        Assembly asm = Assembly.GetExecutingAssembly();

        var controllersInfos = asm.GetTypes() // Get all types from assembly code
            .Where(type => typeof(Controller).IsAssignableFrom(type)) // Get all types, that are controllers
            .Where(controller => controller.ToString().Contains("WebApp.Areas.Admin.Controllers.")) // Filter admin area controllers
            .ToList();

        var controllersInfoStrings = new List<string>();

        foreach (var controllersInfo in controllersInfos)
        {
            var controllerAsString = controllersInfo.ToString();
            controllerAsString = controllerAsString.Split("WebApp.Areas.Admin.Controllers.")[1];
            controllerAsString = controllerAsString.Split("Controller")[0];
            controllersInfoStrings.Add(controllerAsString);
        }

        return controllersInfoStrings;
    }
}