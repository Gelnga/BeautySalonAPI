using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.DAL.DTO.Appointment, App.Domain.Appointment>().ReverseMap();
        CreateMap<App.DAL.DTO.BlogPost, App.Domain.BlogPost>().ReverseMap();
        CreateMap<App.DAL.DTO.JobPosition, App.Domain.JobPosition>().ReverseMap();
        CreateMap<App.DAL.DTO.Salon, App.Domain.Salon>().ReverseMap();
        CreateMap<App.DAL.DTO.SalonService, App.Domain.SalonService>().ReverseMap();
        CreateMap<App.DAL.DTO.SalonWorker, App.Domain.SalonWorker>().ReverseMap();
        CreateMap<App.DAL.DTO.Service, App.Domain.Service>().ReverseMap();
        CreateMap<App.DAL.DTO.Unit, App.Domain.Unit>().ReverseMap();
        CreateMap<App.DAL.DTO.WorkDay, App.Domain.WorkDay>().ReverseMap();
        CreateMap<App.DAL.DTO.Worker, App.Domain.Worker>().ReverseMap();
        CreateMap<App.DAL.DTO.WorkSchedule, App.Domain.WorkSchedule>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.RefreshToken, App.Domain.Identity.RefreshToken>().ReverseMap();
    }
}