using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using AutoMapper;

namespace WebApp;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Appointment, App.BLL.DTO.Appointment>().ReverseMap();
        CreateMap<BlogPost, App.BLL.DTO.BlogPost>().ReverseMap();
        CreateMap<JobPosition, App.BLL.DTO.JobPosition>().ReverseMap();
        CreateMap<Salon, App.BLL.DTO.Salon>().ReverseMap();
        CreateMap<SalonService, App.BLL.DTO.SalonService>().ReverseMap();
        CreateMap<SalonWorker, App.BLL.DTO.SalonWorker>().ReverseMap();
        CreateMap<Service, App.BLL.DTO.Service>().ReverseMap();
        CreateMap<Unit, App.BLL.DTO.Unit>().ReverseMap();
        CreateMap<WorkDay, App.BLL.DTO.WorkDay>().ReverseMap();
        CreateMap<Worker, App.BLL.DTO.Worker>().ReverseMap();
        CreateMap<WorkSchedule, App.BLL.DTO.WorkSchedule>().ReverseMap();
        CreateMap<AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<RefreshToken, App.BLL.DTO.Identity.RefreshToken>().ReverseMap();

        // Custom DTOs mappers
        CreateMap<ServiceWithSalonServiceData, App.BLL.DTO.Service>().ReverseMap();
        CreateMap<WorkerWithSalonServiceData, App.BLL.DTO.Worker>().ReverseMap();
        
        // Types mapping config
        CreateMap<string, DateOnly>().ConvertUsing<StringToDateOnlyTypeConverter>();
        CreateMap<string, TimeSpan>().ConvertUsing<StringToTimeSpanTypeConverter>();
        CreateMap<DateOnly, string>().ConvertUsing<DateOnlyToStringTypeConverter>();
        CreateMap<TimeSpan, string>().ConvertUsing<TimeSpanToStringTypeConverter>();
    }
    
    public class StringToDateOnlyTypeConverter : ITypeConverter<string, DateOnly>
    {
        public DateOnly Convert(string source, DateOnly destination, ResolutionContext context)
        {
            return DateOnly.FromDateTime(DateTime.Parse(source));
        }
    }
    
    public class StringToTimeSpanTypeConverter : ITypeConverter<string, TimeSpan>
    {
        public TimeSpan Convert(string source, TimeSpan destination, ResolutionContext context)
        {
            return TimeSpan.Parse(source);
        }
    }
    
    public class DateOnlyToStringTypeConverter : ITypeConverter<DateOnly, string>
    {
        public string Convert(DateOnly source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
    
    public class TimeSpanToStringTypeConverter : ITypeConverter<TimeSpan, string>
    {
        public string Convert(TimeSpan source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
}