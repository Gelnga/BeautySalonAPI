using System.Reflection;
using App.DAL;
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