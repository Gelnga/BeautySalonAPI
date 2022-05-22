using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.Domain;
using BasePublicAPI;

namespace App.Public.DTO.v1;

public class BlogPost : PublicDTOBase
{
    public Guid? WorkerId { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public bool IsArticle { get; set; }
    public string Content { get; set; } = default!;
}