using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class BlogPost : DomainEntityBaseId<AppUser>
{
    public Guid? WorkerId { get; set; }
    public Worker? Worker { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public bool IsArticle { get; set; }
    public string Content { get; set; } = default!;
}