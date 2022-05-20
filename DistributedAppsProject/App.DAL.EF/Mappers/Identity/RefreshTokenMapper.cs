using App.DAL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers.Identity;

public class RefreshTokenMapper : BaseMapper<RefreshToken, App.Domain.Identity.RefreshToken>
{
    public RefreshTokenMapper(IMapper mapper) : base(mapper)
    {
    }
}