using System.Collections;
using System.Reflection;
using App.DAL;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using App.Enums;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class WebAppHelperMethods
{
    private static Guid _adminUserId = default!;
    public static async Task SetUpAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
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
            var roles = new []
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
                _adminUserId = user.Id;
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

        if (config.GetValue<bool>("DataInitialization:SeedData"))
        {
            var scheduleWeek = new WorkSchedule
            {
                AppUserId = _adminUserId,
                Name = "scheduleWeekSalon",
                IsWeek = true
            };

            var savedScheduleWeek = context.Add(scheduleWeek);
            foreach (var weekDay in Enum.GetValues(typeof(Days)))
            {
                var weekDayAsEnum = weekDay is Days ? (Days) weekDay : Days.Monday;
                var workDay = new WorkDay
                {
                    AppUserId = _adminUserId,
                    WorkScheduleId = savedScheduleWeek.Entity.Id,
                    WorkDayStart = new TimeOnly(12, 00),
                    WorkDayEnd = new TimeOnly(20, 00),
                    WeekDay = weekDayAsEnum
                };
                await context.AddAsync(workDay);
            }

            var salon1 = new Salon()
            {
                AppUserId = _adminUserId,
                WorkScheduleId = savedScheduleWeek.Entity.Id,
                Name = "Salon 1",
                Description = "This is a first salon",
                Address = "Here should be a salon address",
                GoogleMapsLink = "https://goo.gl/maps/QnVBQCCQ6yfe4XGi8",
                Email = "salon@mail.com",
                PhoneNumber = "+372 51247274"
            };
            context.Add(salon1);

            await context.SaveChangesAsync();

            var salons = await context.Salons.ToListAsync();
            var schedules = await context.WorkSchedules.ToListAsync();
            var workDays = await context.WorkDays.ToListAsync();
            
            foreach (var salon in salons)
            {
                Console.WriteLine("SALONS");
                Console.WriteLine($"{salon.Name} {salon.Description}");
            }

            foreach (var schedule in schedules)
            {
                Console.WriteLine("SCHEDULES");
                Console.WriteLine($"{schedule.Name} {schedule.IsWeek}");
            }

            foreach (var workDay in workDays)
            {
                Console.WriteLine("WORKDAYS");
                var schedule = await context.WorkSchedules.FirstOrDefaultAsync(e => e.Id == workDay.WorkScheduleId);
                Console.WriteLine($"{workDay.WeekDay} {schedule!.Name}");
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