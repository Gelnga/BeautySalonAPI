using App.Domain;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Composed keys functionality, for some reason doesn't generate correctly with controllers,
        // so I decided to do not use composed keys for now 
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

        // Remove cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    
    public override int SaveChanges()
    {
        FixEntitiesDateTime(this);
        
        return base.SaveChanges(); 
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        FixEntitiesDateTime(this);
        
        return base.SaveChangesAsync(cancellationToken);
    }

    private void FixEntitiesDateTime(ApplicationDbContext context)
    {
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });

        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }
}