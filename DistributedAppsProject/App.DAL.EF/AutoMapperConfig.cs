using App.DAL.DTO.Identity;
using App.Resources.App.Domain;
using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.DAL.DTO.JobPosition, App.Domain.JobPosition>().ReverseMap();
        CreateMap<App.DAL.DTO.WorkDay, App.Domain.WorkDay>().ReverseMap();
        CreateMap<App.DAL.DTO.WorkSchedule, App.Domain.WorkSchedule>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
        CreateMap<App.DAL.DTO.Identity.RefreshToken, App.Domain.Identity.RefreshToken>().ReverseMap();
    }
}