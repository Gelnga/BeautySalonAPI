using AutoMapper;
using Base.DAL;

namespace WebApp.Mappers;

public class AppointmentMapper : BaseMapper<App.Public.DTO.v1.Appointment, App.BLL.DTO.Appointment>
{
    public AppointmentMapper(IMapper mapper) : base(mapper)
    {
    }
}