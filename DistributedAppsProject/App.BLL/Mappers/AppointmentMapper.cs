using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class AppointmentMapper : BaseMapper<App.BLL.DTO.Appointment, App.DAL.DTO.Appointment>
{
    public AppointmentMapper(IMapper mapper) : base(mapper)
    {
    }
}