using System.Globalization;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using App.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.WebApp;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    private static bool dbInitialized = false;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
         builder.ConfigureServices(ConfigureServices);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // find DbContext
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

        // if found - remove
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        // and new DbContext
        services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });

        // data seeding
        // create db and seed data
        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

        db.Database.EnsureCreated();

        var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scopedServices.GetRequiredService<RoleManager<AppRole>>();

        SeedData(db, userManager, roleManager);
        var test = db.Salons.ToList();
                
        db.Database.EnsureCreated();
    }

    private void SeedData(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        var adminUserId = new Guid("00000000-0000-0000-0000-000000000000");

        if (userManager == null || roleManager == null)
            throw new NullReferenceException("userManager or roleManager cannot be null!");
        var roles = new[]
        {
            "admin",
            "worker",
            "user"
        };

        foreach (var roleInfo in roles)
        {
            var role = roleManager.FindByNameAsync(roleInfo).Result;
            if (role != null) continue;

            var identityResult = roleManager
                .CreateAsync(new AppRole {Name = roleInfo, NormalizedName = roleInfo.ToUpper()}).Result;
            if (!identityResult.Succeeded)
            {
                throw new ApplicationException("Role creation failed");
            }
        }

        var users = new (string username, string password, string roles, string FirstName, string LastName)[]
        {
            ("admin@itcollege.ee", "Kala.maja1!", "user, admin", "Admin", "Admin"),
            ("admin2@itcollege.ee", "Kala.maja1!", "user, admin", "Admin2", "Admin2"),
            ("test.user@gmail.com", "Kala.maja1!", "user", "Test", "User")
        };

        foreach (var userInfo in users)
        {
            var user = userManager.FindByEmailAsync(userInfo.username).Result;
            if (user != null) continue;

            user = new AppUser()
            {
                Email = userInfo.username,
                UserName = userInfo.username,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                EmailConfirmed = true,
            };
            var identityResult = userManager.CreateAsync(user, userInfo.password).Result;

            if (adminUserId.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                adminUserId = user.Id;
            }

            if (!identityResult.Succeeded)
            {
                throw new ApplicationException("Cannot create user!");
            }

            if (!string.IsNullOrWhiteSpace(userInfo.roles))
            {
                var identityResultRole = userManager.AddToRolesAsync(user,
                    userInfo.roles.Split(",").Select(r => r.Trim())
                );
            }
        }

        // Salon work schedule
        var salonScheduleWeek = new WorkSchedule
        {
            OwnerId = adminUserId,
            Name = "salonScheduleWeek",
            IsWeek = true
        };

        // WorkDays for salon work schedule
        var savedSalonScheduleWeek = context.Add(salonScheduleWeek);
        foreach (var weekDay in Enum.GetValues(typeof(Days)))
        {
            var weekDayAsEnum = weekDay is Days ? (Days) weekDay : Days.Monday;
            var workDay = new WorkDay
            {
                OwnerId = adminUserId,
                WorkScheduleId = savedSalonScheduleWeek.Entity.Id,
                WorkDayStart = new TimeSpan(12, 00, 00),
                WorkDayEnd = new TimeSpan(20, 00, 00),
                WeekDay = weekDayAsEnum
            };
            context.Add(workDay);
        }
        
        // First salon
        var salon1 = new Salon
        {
            OwnerId = adminUserId,
            WorkScheduleId = savedSalonScheduleWeek.Entity.Id,
            Name = "Salon 1",
            Description = "This is a first salon",
            Address = "1413 Oak Avenue",
            GoogleMapsLink = "https://goo.gl/maps/QnVBQCCQ6yfe4XGi8",
            Email = "salon1@gmail.com",
            PhoneNumber = "+372 51247274"
        };
        context.Add(salon1);
        
        // Second salon
        var salon2 = new Salon
        {
            OwnerId = adminUserId,
            WorkScheduleId = savedSalonScheduleWeek.Entity.Id,
            Name = "Salon 2",
            Description = "This is a second salon",
            Address = "4457 Lynn Street",
            GoogleMapsLink = "https://goo.gl/maps/a2SnhwoCk4WBazqk7",
            Email = "salon2@gmail.com",
            PhoneNumber = "+372 51247274"
        };
        context.Add(salon2);
        
        // Default work schedule for workers
        var workerDefaultScheduleWeek = new WorkSchedule
        {
            OwnerId = adminUserId,
            Name = "workerDefaultScheduleWeek",
            IsWeek = true
        };
        
        // WorkDays for default workers work schedule
        var savedWorkerDefaultScheduleWeek = context.Add(workerDefaultScheduleWeek);
        foreach (var weekDay in Enum.GetValues(typeof(Days)))
        {
            var weekDayAsEnum = weekDay is Days ? (Days) weekDay : Days.Monday;
            var workDay = new WorkDay
            {
                OwnerId = adminUserId,
                WorkScheduleId = savedWorkerDefaultScheduleWeek.Entity.Id,
                WorkDayStart = new TimeSpan(12, 00, 00),
                WorkDayEnd = new TimeSpan(20, 00, 00),
                LunchBreakStartTime = new TimeSpan(13, 00, 00),
                LunchBreakEndTime = new TimeSpan(14, 00, 00),
                WeekDay = weekDayAsEnum
            };
            context.Add(workDay);
        }
        
        // Job positions
        var manicurist = new JobPosition
        {
            OwnerId = adminUserId,
            Name = "Manicurist"
        };
        context.Add(manicurist);
        
        var hairdresser = new JobPosition
        {
            OwnerId = adminUserId,
            Name = "Hair dresser"
        };
        context.Add(hairdresser);
        
        var pedicurist = new JobPosition
        {
            OwnerId = adminUserId,
            Name = "Pedicurist"
        };
        context.Add(pedicurist);
        
        // First list of workers
        var workers1 = new List<Worker>();
        var worker1 = new Worker
        {
            OwnerId = adminUserId,
            JobPositionId = manicurist.Id,
            WorkScheduleId = savedWorkerDefaultScheduleWeek.Entity.Id,
            FirstName = "Kiara",
            LastName = "Patterson",
            Email = "kiara.patterson@gmail.com",
            PhoneNumber = "+372 5617240"
        };
        workers1.Add(context.Add(worker1).Entity);
        
        var worker2 = new Worker
        {
            OwnerId = adminUserId,
            JobPositionId = hairdresser.Id,
            WorkScheduleId = savedWorkerDefaultScheduleWeek.Entity.Id,
            FirstName = "Sylvia",
            LastName = "Maddox",
            Email = "sylvia.maddox@gmail.com",
            PhoneNumber = "+372 5630540"
        };
        workers1.Add(context.Add(worker2).Entity);
        
        // Second list of workers
        var workers2 = new List<Worker>();
        var worker3 = new Worker
        {
            OwnerId = adminUserId,
            JobPositionId = manicurist.Id,
            WorkScheduleId = savedWorkerDefaultScheduleWeek.Entity.Id,
            FirstName = "Alonzo",
            LastName = "Hill",
            Email = "alonzo.hill@gmail.com",
            PhoneNumber = "+372 5532920"
        };
        workers2.Add(context.Add(worker3).Entity);
        
        var worker4 = new Worker
        {
            OwnerId = adminUserId,
            JobPositionId = pedicurist.Id,
            WorkScheduleId = savedWorkerDefaultScheduleWeek.Entity.Id,
            FirstName = "Wendy",
            LastName = "Velez",
            Email = "wendy.velez@gmail.com",
            PhoneNumber = "+372 5517245"
        };
        workers2.Add(context.Add(worker4).Entity);
        
        // Seed workers identity
        var allWorkers = workers1.Concat(workers2).ToList();
        var workersUsers =
            new (string username, string password, string roles, Worker worker)[allWorkers.Count];
        for (int i = 0; i < allWorkers.Count; i++)
        {
            var worker = allWorkers[i];
            workersUsers[i] = (worker.Email, "Kala.maja1!", "worker", worker);
        }
        
        foreach (var userInfo in workersUsers)
        {
            var user = userManager.FindByEmailAsync(userInfo.username).Result;
            if (user != null) continue;
        
            user = new AppUser()
            {
                // WorkerId = userInfo.worker.Id,
                Email = userInfo.username,
                UserName = userInfo.username,
                FirstName = userInfo.worker.FirstName,
                LastName = userInfo.worker.LastName,
                WorkerId = userInfo.worker.Id,
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
        
        // Adding workers for salons
        var salon1Workers = new List<SalonWorker>();
        var salon2Workers = new List<SalonWorker>();
        foreach (var worker in workers1)
        {
            var salonWorker = new SalonWorker
            {
                OwnerId = adminUserId,
                SalonId = salon1.Id,
                WorkerId = worker.Id
            };
            context.SalonWorkers.Add(salonWorker);
            salon1Workers.Add(salonWorker);
        }
        
        foreach (var worker in workers2)
        {
            var salonWorker = new SalonWorker
            {
                OwnerId = adminUserId,
                SalonId = salon2.Id,
                WorkerId = worker.Id
            };
            context.SalonWorkers.Add(salonWorker);
            salon2Workers.Add(salonWorker);
        }
        
        // Adding services
        var service1 = new Service
        {
            OwnerId = adminUserId,
            Name = "Pedicure",
            Description = "A pedicure is a cosmetic treatment of the feet and toenails."
        };
        context.Add(service1);
        
        var service2 = new Service
        {
            OwnerId = adminUserId,
            Name = "Haircut",
            Description = "A basic haircut service."
        };
        context.Add(service2);
        
        var service3 = new Service
        {
            OwnerId = adminUserId,
            Name = "Hair dying",
            Description = "A service of dyeing hair. Every salon has a wide variety of hair dyes to choose"
        };
        context.Add(service3);
        
        var service4 = new Service
        {
            OwnerId = adminUserId,
            Name = "Manicure",
            Description = "A manicure is a mostly cosmetic beauty treatment for the fingernails and hands."
        };
        context.Add(service4);
        
        // Adding Unit (euro)
        var euro = new Unit
        {
            OwnerId = adminUserId,
            Name = "Euro"
        };
        var addedEuro = context.Add(euro);
        
        // Adding services for salons
        var salonService1 = new SalonService
        {
            OwnerId = adminUserId,
            SalonId = salon1.Id,
            ServiceId = service4.Id,
            SalonWorkerId = salon1Workers.First(e => e.WorkerId == worker1.Id).Id,
            ServiceDuration = new TimeSpan(1, 30, 00),
            Price = 40,
            UnitId = addedEuro.Entity.Id
        };
        context.Add(salonService1);
        
        var salonService2 = new SalonService
        {
            OwnerId = adminUserId,
            SalonId = salon1.Id,
            ServiceId = service2.Id,
            SalonWorkerId = salon1Workers.First(e => e.WorkerId == worker2.Id).Id,
            ServiceDuration = new TimeSpan(1, 00, 00),
            Price = 15,
            UnitId = addedEuro.Entity.Id
        };
        context.Add(salonService2);
        
        var salonService3 = new SalonService
        {
            OwnerId = adminUserId,
            SalonId = salon1.Id,
            ServiceId = service3.Id,
            SalonWorkerId = salon1Workers.First(e => e.WorkerId == worker2.Id).Id,
            ServiceDuration = new TimeSpan(0, 30, 00),
            Price = 16,
            UnitId = addedEuro.Entity.Id
        };
        context.Add(salonService3);
        
        var salonService4 = new SalonService
        {
            OwnerId = adminUserId,
            SalonId = salon2.Id,
            ServiceId = service4.Id,
            SalonWorkerId = salon2Workers.First(e => e.WorkerId == worker3.Id).Id,
            Price = 30,
            ServiceDuration = new TimeSpan(1, 30, 00),
            UnitId = addedEuro.Entity.Id
        };
        context.Add(salonService4);
        
        var salonService5 = new SalonService
        {
            OwnerId = adminUserId,
            SalonId = salon2.Id,
            ServiceId = service1.Id,
            SalonWorkerId = salon2Workers.First(e => e.WorkerId == worker4.Id).Id,
            Price = 35,
            ServiceDuration = new TimeSpan(2, 00, 00),
            UnitId = addedEuro.Entity.Id
        };
        context.Add(salonService5);

        context.SaveChanges();
    }
}

public class MockUserManager : UserManager<AppUser>
{
    public MockUserManager()
        : base(new Mock<IUserStore<AppUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<AppUser>>().Object,
            new IUserValidator<AppUser>[0],
            new IPasswordValidator<AppUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<AppUser>>>().Object)
    {
    }
}

public class MockSingInManager : SignInManager<AppUser>
{
    public MockSingInManager()
        : base(
            new MockUserManager(),
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<AppUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<AppUser>>().Object)
    {
    }
}

public class MockRoleManager : RoleManager<AppRole>
{
    public MockRoleManager() : base(
        new Mock<IRoleStore<AppRole>>().Object,
        new IRoleValidator<AppRole>[0],
        new Mock<ILookupNormalizer>().Object,
        new Mock<IdentityErrorDescriber>().Object,
        new Mock<ILogger<RoleManager<AppRole>>>().Object)
    {
    }
}