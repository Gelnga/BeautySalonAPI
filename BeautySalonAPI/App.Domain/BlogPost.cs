using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class BlogPost : DomainEntityBaseMetaId
{
    public Guid? WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.BlogPost), Name = "Worker")]
    public Worker? Worker { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.App.Domain.BlogPost), Name = "Name")]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = default!;

    [MaxLength(8192)]
    [Display(ResourceType = typeof(Resources.App.Domain.BlogPost), Name = "Content")]
    [Column(TypeName = "jsonb")]
    public LangStr Content { get; set; } = default!;
}