using App.Domain;

namespace WebApp.DTO;

public class WorkDayDTO : WorkDay
{
    public new string WeekDay { get; set; } = default!;
    
    public WorkDayDTO()
    {
    }

    public WorkDayDTO(WorkDay workDay)
    {
        WeekDay = workDay.WeekDay;
        DTOBinder.BindEntityPropertiesToDtoProperties(workDay, this);
    }

    public WorkDay ToEntity()
    {
        var entity = new WorkDay
        {
            WeekDay = WeekDay
        };
        
        DTOBinder.BindDtoPropertiesToEntityProperties(this, entity);
        return entity;
    }
}