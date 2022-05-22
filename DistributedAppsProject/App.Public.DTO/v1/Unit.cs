using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using Base.Domain;
using BasePublicAPI;

namespace App.Public.DTO.v1;

public class Unit : PublicDTOBase
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
}