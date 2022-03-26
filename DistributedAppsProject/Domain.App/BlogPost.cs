using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Resources = App.Resources.App.Domain;

namespace Domain.App;

public class BlogPost : BaseEntityId
{
    public Guid? WorkerId { get; set; }
    [Display(ResourceType = typeof(Resources.BlogPost), Name = "Worker")]
    public Worker? Worker { get; set; }

    [MaxLength(256)]
    [Display(ResourceType = typeof(Resources.BlogPost), Name = "Name")]
    public String Name { get; set; } = default!;

    [Display(ResourceType = typeof(Resources.BlogPost), Name = "IsArticle")]
    public bool IsArticle { get; set; }
    [Display(ResourceType = typeof(Resources.BlogPost), Name = "Content")]
    public String Content { get; set; } = default!;
}