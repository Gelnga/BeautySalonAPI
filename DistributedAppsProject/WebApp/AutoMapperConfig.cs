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
        CreateMap<ImageObject, App.BLL.DTO.ImageObject>().ReverseMap();
        CreateMap<Image, App.BLL.DTO.Image>().ReverseMap();
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
    }
}