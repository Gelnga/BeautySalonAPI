using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Appointment> Appointments { get; set; } = default!;
    public DbSet<BlogPost> BlogPosts { get; set; } = default!;
    public DbSet<Image> Images { get; set; } = default!;
    public DbSet<ImageObject> ImageObjects { get; set; } = default!;
    public DbSet<JobPosition> JobPositions { get; set; } = default!;
    public DbSet<Salon> Salons { get; set; } = default!;
    public DbSet<SalonService> SalonServices { get; set; } = default!;
    public DbSet<SalonWorker> SalonWorkers { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;
    public DbSet<Unit> Units { get; set; } = default!;
    public DbSet<WorkDay> WorkDays { get; set; } = default!;
    public DbSet<Worker> Workers { get; set; } = default!;
    public DbSet<WorkSchedule> WorkSchedules { get; set; } = default!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);
    //     builder.Entity<SalonService>().HasKey(c => new
    //     {
    //         c.SalonId,
    //         c.ServiceId
    //     });
    //     
    //     builder.Entity<SalonWorker>().HasKey(c => new
    //     {
    //         c.SalonId,
    //         c.WorkerId
    //     });
    // }
}