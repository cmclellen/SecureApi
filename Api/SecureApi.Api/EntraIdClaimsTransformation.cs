using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SecureApi.Api;

public class EntraIdClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var claimsIdentity = (ClaimsIdentity)principal.Identity!;

        var nameClaim = principal.FindFirst("name");
        if (!principal.HasClaim(claim => claim.Type == ClaimTypes.Name) && nameClaim is not null)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, nameClaim.Value));
        }
        principal.AddIdentity(claimsIdentity);
        return Task.FromResult(principal);
    }
}