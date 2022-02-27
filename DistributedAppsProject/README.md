# Distributed systems project

Database migration and update
~~~sh
dotnet ef migrations add --project DAL.App --startup-project WebApp Initial
dotnet ef database update --project DAL.App --startup-project WebApp
~~~

Web Controllers
~~~sh
dotnet aspnet-codegenerator controller -name AppointmentsController -actions -m Domain.App.Appointment -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name BlogPostsController -actions -m Domain.App.BlogPost -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ImagesController -actions -m Domain.App.Image -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ImageObjectsController -actions -m Domain.App.ImageObject -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name JobPositionsController -actions -m Domain.App.JobPosition -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SalonsController -actions -m Domain.App.Salon -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SalonServicesController -actions -m Domain.App.SalonService -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SalonWorkersController -actions -m Domain.App.SalonWorker -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ServicesController -actions -m Domain.App.Service -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UnitsController -actions -m Domain.App.Unit -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkDaysController -actions -m Domain.App.WorkDay -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkerController -actions -m Domain.App.Worker -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkScheduleController -actions -m Domain.App.WorkSchedule -dc ApplicationDbContext -outDir Areas\Admin\Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~