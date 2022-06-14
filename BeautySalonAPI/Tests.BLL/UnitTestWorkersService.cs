using App.BLL;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain;
using App.Domain.Identity;
using App.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp.Mappers;
using Xunit;
using Xunit.Abstractions;
using AutoMapperConfig = WebApp.AutoMapperConfig;
using SalonService = App.Domain.SalonService;

namespace Tests.BLL;

public class UnitTestWorkersService
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IAppBLL _bll;
    private readonly ApplicationDbContext _ctx;
    private readonly IMapper _mapper;
    private readonly App.BLL.Mappers.WorkerMapper _workerMapper;
    private readonly WorkerWithSalonServiceDataMapper _workerWithSalonServiceDataMapper;

    public UnitTestWorkersService(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var configBllToDal = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<App.DAL.DTO.Appointment, App.BLL.DTO.Appointment>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.BlogPost, App.BLL.DTO.BlogPost>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.JobPosition, App.BLL.DTO.JobPosition>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Salon, App.BLL.DTO.Salon>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.SalonService, App.BLL.DTO.SalonService>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.SalonWorker, App.BLL.DTO.SalonWorker>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Service, App.BLL.DTO.Service>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Unit, App.BLL.DTO.Unit>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.WorkDay, App.BLL.DTO.WorkDay>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Worker, App.BLL.DTO.Worker>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.WorkSchedule, App.BLL.DTO.WorkSchedule>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Identity.AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Identity.RefreshToken, App.BLL.DTO.Identity.RefreshToken>().ReverseMap();

            // Types mapping config
            cfg.CreateMap<string, DateOnly>().ConvertUsing<AutoMapperConfig.StringToDateOnlyTypeConverter>();
            cfg.CreateMap<string, TimeSpan>().ConvertUsing<AutoMapperConfig.StringToTimeSpanTypeConverter>();
            cfg.CreateMap<DateOnly, string>().ConvertUsing<AutoMapperConfig.DateOnlyToStringTypeConverter>();
            cfg.CreateMap<TimeSpan, string>().ConvertUsing<AutoMapperConfig.TimeSpanToStringTypeConverter>();
        });
        
        var configDalToDomain = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<App.DAL.DTO.Appointment, Appointment>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.BlogPost, BlogPost>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.JobPosition, JobPosition>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Salon, Salon>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.SalonService, SalonService>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.SalonWorker, SalonWorker>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Service, Service>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Unit, Unit>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.WorkDay, WorkDay>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Worker, Worker>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.WorkSchedule, WorkSchedule>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Identity.AppUser, AppUser>().ReverseMap();
            cfg.CreateMap<App.DAL.DTO.Identity.RefreshToken, RefreshToken>().ReverseMap();

            // Types mapping config
            cfg.CreateMap<string, DateOnly>().ConvertUsing<AutoMapperConfig.StringToDateOnlyTypeConverter>();
            cfg.CreateMap<string, TimeSpan>().ConvertUsing<AutoMapperConfig.StringToTimeSpanTypeConverter>();
            cfg.CreateMap<DateOnly, string>().ConvertUsing<AutoMapperConfig.DateOnlyToStringTypeConverter>();
            cfg.CreateMap<TimeSpan, string>().ConvertUsing<AutoMapperConfig.TimeSpanToStringTypeConverter>();
        });
        _mapper = new Mapper(configBllToDal);
        _workerMapper = new App.BLL.Mappers.WorkerMapper(_mapper);
        _workerWithSalonServiceDataMapper = new WorkerWithSalonServiceDataMapper(_mapper);

        // set up mock db - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new ApplicationDbContext(optionsBuilder.Options);

        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        
        var mapperDalToDomain = new Mapper(configDalToDomain);
        var uow = new AppUOW(_ctx, mapperDalToDomain); 
        _bll = new AppBLL(uow, _mapper);
        setUpData(_ctx);
    }

    private App.BLL.DTO.Worker GetDummyWorker(ApplicationDbContext context)
    {
        var jobPosition = _ctx.JobPositions.First(e => true);
        var workSchedule = _ctx.WorkSchedules.First(e => true);

        var worker = new App.BLL.DTO.Worker
        {
            JobPositionId = jobPosition.Id,
            WorkScheduleId = workSchedule.Id,
            Email = "Aloy.hije@gmail.com",
            FirstName = "Aloy",
            LastName = "Hije",
            PhoneNumber = "+372 71264890",
            Commentary = null
        };

        return worker;
    }
    
    [Fact]
    public void TestAdd()
    {
        var worker = GetDummyWorker(_ctx);
        var added = _bll.Workers.Add(worker);
        _bll.SaveChanges();
        
        Assert.Equal("Aloy", added.FirstName);
        Assert.Equal("Hije", added.LastName);
    }

    [Fact]
    public void TestUpdate()
    {
        var worker = GetDummyWorker(_ctx);
        worker.LastName = "Nala";
        var updated = _bll.Workers.Update(worker);
        _bll.SaveChanges();
        
        Assert.Equal("Nala", updated.LastName);
    }

    [Fact]
    public void TestRemoveByEntity()
    {
        var worker = GetDummyWorker(_ctx);
        _bll.Workers.Add(worker);
        _bll.SaveChanges();
        var removed = _bll.Workers.Remove(worker);
        
        Assert.Equal("Aloy", removed.FirstName);
        Assert.Equal("Hije", removed.LastName);
    }

    [Fact]
    public void TestRemoveById()
    {
        var worker = _bll.Workers.GetAll().First(e => true);
        var firstName = worker.FirstName;
        var lastName = worker.LastName;
        _ctx.ChangeTracker.Clear();
        var removed2 = _bll.Workers.Remove(worker.Id);
        
        Assert.Equal(firstName, removed2.FirstName);
        Assert.Equal(lastName, removed2.LastName);
    }

    [Fact]
    public void TestFirstOrDefault()
    {
        var worker = _bll.Workers.GetAll().First(e => true);
        var firstName = worker.FirstName;
        var lastName = worker.LastName;
        var firstOrDefault = _bll.Workers.FirstOrDefault(worker.Id);
        
        Assert.Equal(firstName, firstOrDefault!.FirstName);
        Assert.Equal(lastName, firstOrDefault.LastName);
    }

    [Fact]
    public void TestGetAll()
    {
        var worker = GetDummyWorker(_ctx);
        var allCount = _bll.Workers.GetAll().Count();
        _bll.Workers.Add(worker);
        _bll.SaveChanges();
        var allAfterCount = _bll.Workers.GetAll().Count();
        Assert.True(allAfterCount - allCount == 1);
    }

    [Fact]
    public void TestExists()
    {
        var worker = GetDummyWorker(_ctx);
        worker = _bll.Workers.Add(worker);
        _bll.SaveChanges();
        var exists = _bll.Workers.Exists(worker.Id);
        
        Assert.True(exists);
    }

    [Fact]
    public async Task TestFirstOrDefaultAsync()
    {
        var worker = GetDummyWorker(_ctx);
        worker = _bll.Workers.Add(worker);
        await _bll.SaveChangesAsync();
        var firstOrDefault = await _bll.Workers.FirstOrDefaultAsync(worker.Id);
        
        Assert.True(firstOrDefault!.Id == worker.Id);
    }

    [Fact]
    public async Task TestGetAllAsync()
    {
        var worker = GetDummyWorker(_ctx);
        var allCount = (await _bll.Workers.GetAllAsync()).Count();
        worker = _bll.Workers.Add(worker);
        await _bll.SaveChangesAsync();
        var afterCount = (await _bll.Workers.GetAllAsync()).Count();
        
        Assert.True(afterCount - allCount == 1);
    }

    [Fact]
    public async Task TestExistsAsync()
    {
        var worker = GetDummyWorker(_ctx);
        worker = _bll.Workers.Add(worker);
        await _bll.SaveChangesAsync();
        var exists = await _bll.Workers.ExistsAsync(worker.Id);
        
        Assert.True(exists);
    }
    
    [Fact]
    public async Task TestRemoveAsync()
    {
        var worker = GetDummyWorker(_ctx);
        worker = _bll.Workers.Add(worker);
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();
        var removed = await _bll.Workers.RemoveAsync(worker.Id);
        
        Assert.True(removed.Id == worker.Id);
    }

    private void setUpData(ApplicationDbContext context)
    {
        var appUser = new AppUser
        {
            Email = "Test@gmail.com",
            UserName = "Test@gmail.com",
            FirstName = "Test",
            LastName = "Test"
        };
        var saved = context.Add(appUser);
        var adminUserId = saved.Entity.Id;
        
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