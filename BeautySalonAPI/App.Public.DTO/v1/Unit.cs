using System.ComponentModel.DataAnnotations;
using Base.Public.API;

namespace App.Public.DTO.v1;

public class Unit : PublicDTOBase
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public string UnitSymbolCode { get; set; } = default!;
}