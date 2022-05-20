using App.DAL.DTO.Identity;
using App.Resources.App.Domain;
using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<JobPosition, Domain.JobPosition>().ReverseMap();
        CreateMap<WorkDay, Domain.WorkDay>().ReverseMap();
        CreateMap<WorkSchedule, Domain.WorkSchedule>().ReverseMap();
        CreateMap<AppUser, Domain.Identity.AppUser>().ReverseMap();
        CreateMap<RefreshToken, Domain.Identity.RefreshToken>().ReverseMap();
    }
}