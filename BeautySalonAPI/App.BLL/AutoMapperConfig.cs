using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.BLL.DTO.Appointment, App.DAL.DTO.Appointment>().ReverseMap();
        CreateMap<App.BLL.DTO.BlogPost, App.DAL.DTO.BlogPost>().ReverseMap();
        CreateMap<App.BLL.DTO.JobPosition, App.DAL.DTO.JobPosition>().ReverseMap();
        CreateMap<App.BLL.DTO.Salon, App.DAL.DTO.Salon>().ReverseMap();
        CreateMap<App.BLL.DTO.SalonService, App.DAL.DTO.SalonService>().ReverseMap();
        CreateMap<App.BLL.DTO.SalonWorker, App.DAL.DTO.SalonWorker>().ReverseMap();
        CreateMap<App.BLL.DTO.Service, App.DAL.DTO.Service>().ReverseMap();
        CreateMap<App.BLL.DTO.Unit, App.DAL.DTO.Unit>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay>().ReverseMap();
        CreateMap<App.BLL.DTO.Worker, App.DAL.DTO.Worker>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkSchedule, App.DAL.DTO.WorkSchedule>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.RefreshToken, App.DAL.DTO.Identity.RefreshToken>().ReverseMap();
    }
}