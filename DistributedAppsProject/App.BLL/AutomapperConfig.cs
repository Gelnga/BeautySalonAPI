using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.JobPosition, App.DAL.DTO.JobPosition>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkDay, App.DAL.DTO.WorkDay>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkSchedule, App.DAL.DTO.WorkSchedule>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
    }
}