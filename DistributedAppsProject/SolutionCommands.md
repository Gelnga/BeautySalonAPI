# Solution commands
### This file contains commands, that can be helpful during a development process (placeholder controllers generation, adding database migration, etc.) 

Database migration and update
~~~sh
dotnet ef database drop --project App.DAL.EF --startup-project WebApp
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp Initial
dotnet ef database update --project App.DAL.EF --startup-project WebApp
~~~

Web Controllers
~~~sh
cd WebApp
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

### WebAPI Controllers

~~~sh
cd WebApp
dotnet aspnet-codegenerator controller -name AppointmnetsController -m App.Domain.Appointment -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name BlogPostsController -m App.Domain.BlogPost -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ImagesController -m App.Domain.Image -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ImageObjectsController -m App.Domain.ImageObject -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name JobPositionsController -m App.Domain.JobPosition -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name SalonsController -m App.Domain.Salon -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name SalonServicesController -m App.Domain.SalonService -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name SalonWorkersController -m App.Domain.SalonWorker -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ServicesController -m App.Domain.Service -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name UnitsController -m App.Domain.Unit -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name WorkDaysController -m App.Domain.WorkDay -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name WorkersController -m App.Domain.Worker -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name WorkSchedulesController -m App.Domain.WorkSchedule -actions -dc ApplicationDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~