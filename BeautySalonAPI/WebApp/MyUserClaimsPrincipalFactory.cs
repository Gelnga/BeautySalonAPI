using System.Security.Claims;
using System.Text.Json;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace WebApp;

public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
{
    
    public MyUserClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
        IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
    }
    
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var roles = await UserManager.GetRolesAsync(user);
        var serialized = JsonSerializer.Serialize(roles);
        
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", serialized));
        identity.AddClaim(new Claim("FirstName", user.FirstName ?? "First name"));
        identity.AddClaim(new Claim("LastName", user.LastName ?? "Last name"));
        if (user.WorkerId != null)
        {
            identity.AddClaim(new Claim("WorkerID", user.WorkerId.Value.ToString()));
        }
        else
        {
            identity.AddClaim(new Claim("WorkerID", ""));
        }

        return identity;
    }
}