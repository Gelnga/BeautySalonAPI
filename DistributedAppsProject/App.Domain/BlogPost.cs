using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Domain;
using Domain.App.Identity;
using Worker = App.Domain.Worker;

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

    [Display(ResourceType = typeof(Resources.App.Domain.BlogPost), Name = "IsArticle")]
    public bool IsArticle { get; set; }
    [Display(ResourceType = typeof(Resources.App.Domain.BlogPost), Name = "Content")]
    [Column(TypeName = "jsonb")]
    public LangStr Content { get; set; } = default!;
}