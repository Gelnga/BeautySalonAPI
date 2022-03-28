using Domain.App;

namespace WebApp.DTO;

public class WorkScheduleDto : WorkSchedule
{
    public new string Name { get; set; } = default!;

    public WorkScheduleDto()
    {
    }

    public WorkScheduleDto(WorkSchedule workSchedule)
    {
        Name = workSchedule.Name;
        DtoBinder.BindEntityPropertiesToDtoProperties(workSchedule, this);
    }
    public WorkSchedule ToEntity()
    {
        var entity = new WorkSchedule
        {
            Name = Name
        };
        
        DtoBinder.BindDtoPropertiesToEntityProperties(this, entity);
        return entity;
    }
}