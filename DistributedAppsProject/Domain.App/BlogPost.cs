using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;

namespace Domain.App;

public class BlogPost : BaseEntityId
{
    public Guid? WorkerId { get; set; }
    public Worker? Worker { get; set; }

    [MaxLength(256)]
    public String Name { get; set; } = default!;

    public bool IsArticle { get; set; }
    public String Content { get; set; } = default!;
}