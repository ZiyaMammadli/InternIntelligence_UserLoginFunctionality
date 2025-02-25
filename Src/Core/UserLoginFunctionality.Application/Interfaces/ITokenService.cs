using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Application.Services;

public interface ITokenService
{
    public Task<JwtSecurityToken> GenerateAccessTokenAsync(AppUser appUser,IList<Role> roles);
    public string GenerateRefreshToken();
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}
