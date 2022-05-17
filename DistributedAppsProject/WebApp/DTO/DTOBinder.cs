namespace WebApp.DTO;

public static class DTOBinder
{
    public static void BindEntityPropertiesToDtoProperties<T>(T entity, T dto)
    {
        var dtoProperties = dto!.GetType().GetProperties();
        foreach (var property in dtoProperties)
        {
            var propertyName = property.Name;
            
            // If a derived object has properties with new keywords and those properties type differs from the base class,
            // then after getting all derived object types there will be 2 entries of this properties. Following line of code 
            // drops those properties
            if (dtoProperties.Count(info => info.Name == propertyName) != 1) continue;
            
            var entityProperty = entity!.GetType().GetProperty(propertyName)!.GetValue(entity);
            property.SetValue(dto, entityProperty);
        }
    }
    
    public static void BindDtoPropertiesToEntityProperties<T>(T dto, T entity)
    {
        var dtoProperties = dto!.GetType().GetProperties();
        foreach (var property in entity!.GetType().GetProperties())
        {
            var propertyName = property.Name;
            if (dtoProperties.Count(info => info.Name == propertyName) != 1) continue;
            
            var dtoProperty = dto.GetType().GetProperty(propertyName)!.GetValue(dto);
            property.SetValue(entity, dtoProperty);
        }
    }
}