using App.Domain;

namespace WebApp.DTO;

public class JobPositionDTO : JobPosition
{

    public new string Name { get; set; } = default!;
    
    public JobPositionDTO()
    {
    }

    public JobPositionDTO(JobPosition jobPosition)
    {
        Name = jobPosition.Name;
        DTOBinder.BindEntityPropertiesToDtoProperties(jobPosition, this);
    }

    public JobPosition ToEntity()
    {
        var entity = new JobPosition
        {
            Name = Name
        };
        
        DTOBinder.BindDtoPropertiesToEntityProperties(this, entity);
        return entity;
    }
}