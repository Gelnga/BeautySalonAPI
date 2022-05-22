using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using AutoMapper;

namespace WebApp;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<JobPosition, App.BLL.DTO.JobPosition>().ReverseMap();
        CreateMap<WorkDay, App.BLL.DTO.WorkDay>().ReverseMap();
        CreateMap<WorkSchedule, App.BLL.DTO.WorkSchedule>().ReverseMap();
        CreateMap<AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<RefreshToken, App.BLL.DTO.Identity.RefreshToken>().ReverseMap();
    }
}