using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class BlogPost : DomainEntityBaseId
{
    public Guid? WorkerId { get; set; }
    public Worker? Worker { get; set; }
    
    public string? WorkerName { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    public string Content { get; set; } = default!;
}