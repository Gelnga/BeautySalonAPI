using System.ComponentModel.DataAnnotations;
using Base.Public.API;

namespace App.Public.DTO.v1;

public class JobPosition : PublicDTOBase
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
}