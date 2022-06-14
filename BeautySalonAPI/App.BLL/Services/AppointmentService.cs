using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class AppointmentService :
    BaseEntityService<App.BLL.DTO.Appointment, App.DAL.DTO.Appointment, IAppointmentRepository>, IAppointmentService
{
    public AppointmentService(IAppointmentRepository repository, IMapper<Appointment, DAL.DTO.Appointment> mapper) :
        base(repository, mapper)
    {
        
    }
}