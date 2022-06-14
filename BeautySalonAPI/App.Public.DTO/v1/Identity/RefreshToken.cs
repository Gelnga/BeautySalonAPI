using System.ComponentModel.DataAnnotations;
using Base.Public.API;

namespace App.Public.DTO.v1.Identity;

public class RefreshToken : PublicDTOBase
{
    [StringLength(36, MinimumLength = 36)]
    public string Token { get; set; } = Guid.NewGuid().ToString();
    // UTC
    public DateTime TokenExpirationDateTime { get; set; } = DateTime.UtcNow.AddDays(7);

    [StringLength(36, MinimumLength = 36)]
    public string? PreviousToken { get; set; }
    // UTC
    public DateTime? PreviousTokenExpirationDateTime { get; set; }
}