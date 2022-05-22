using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class AppointmentMapper : BaseMapper<Appointment, App.Domain.Appointment>
{
    public AppointmentMapper(IMapper mapper) : base(mapper)
    {
    }
}