using App.Domain;
using Domain.App;

namespace WebApp.DTO;

public class WorkScheduleDTO : WorkSchedule
{
    public new string Name { get; set; } = default!;

    public WorkScheduleDTO()
    {
    }

    public WorkScheduleDTO(WorkSchedule workSchedule)
    {
        Name = workSchedule.Name;
        DTOBinder.BindEntityPropertiesToDtoProperties(workSchedule, this);
    }
    public WorkSchedule ToEntity()
    {
        var entity = new WorkSchedule
        {
            Name = Name
        };
        
        DTOBinder.BindDtoPropertiesToEntityProperties(this, entity);
        return entity;
    }
}