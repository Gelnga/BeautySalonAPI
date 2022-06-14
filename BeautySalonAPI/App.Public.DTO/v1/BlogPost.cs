using System.ComponentModel.DataAnnotations;
using Base.Public.API;

namespace App.Public.DTO.v1;

public class BlogPost : PublicDTOBase
{
    public Guid? WorkerId { get; set; }
    
    public string? WorkerName { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public bool IsArticle { get; set; }
    public string Content { get; set; } = default!;
}