using System.ComponentModel;
using System.Security.Claims;

namespace Base.Extensions;

public static class IdentityExtension
{
    public static Guid GetUserId(this ClaimsPrincipal user) => GetUserId<Guid>(user);
    public static TKeyType GetUserId<TKeyType>(this ClaimsPrincipal user)
    {
        if (typeof(TKeyType) != typeof(Guid) ||
            typeof(TKeyType) != typeof(string) ||
            typeof(TKeyType) != typeof(int)
           )
        {
            throw new ApplicationException($"This type of User id {typeof(TKeyType).Name} is not supported!");
        }

        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (idClaim == null)
        {
            throw new NullReferenceException("NameIdentifier claim not found");
        }

        var res = (TKeyType) TypeDescriptor
            .GetConverter(typeof(TKeyType))
            .ConvertFromInvariantString(idClaim.Value)!;
        
        return res;
    }
    
}