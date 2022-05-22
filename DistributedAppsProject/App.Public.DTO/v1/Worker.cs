using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;
using BasePublicAPI;

namespace App.Public.DTO.v1;

public class Worker : PublicDTOBase
{
    public Guid? JobPositionId { get; set; }

    public Guid? WorkScheduleId { get; set; }

    [MaxLength(256)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(256)]
    public string LastName { get; set; } = default!;
    
    [MaxLength(256)]
    public string Email { get; set; } = default!;
    
    [MaxLength(256)]
    public string PhoneNumber { get; set; } = default!;
}