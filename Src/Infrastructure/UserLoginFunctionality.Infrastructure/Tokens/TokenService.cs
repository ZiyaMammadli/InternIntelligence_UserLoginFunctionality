using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserLoginFunctionality.Application.Services;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Infrastructure.Tokens;

public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private TokenSettings _tokenSettings;
    public TokenService(UserManager<AppUser> userManager, IOptions<TokenSettings> options)
    {
        _tokenSettings = options.Value;
        _userManager = userManager;
    }
    public async Task<JwtSecurityToken> GenerateAccessTokenAsync(AppUser appUser, IList<Role> roles)
    {
        List<Claim> Claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),

        };
        var userRoles = await _userManager.GetRolesAsync(appUser);

        Claims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

        var token = new JwtSecurityToken(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            claims: Claims,
            expires: DateTime.UtcNow.AddMinutes(_tokenSettings.AccessTokenValidityInMinutes),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

        await _userManager.AddClaimsAsync(appUser, Claims);
        return token;
    }

    public string GenerateRefreshToken()
    {
        byte[] num = new byte[32];
        using RandomNumberGenerator rng= RandomNumberGenerator.Create();
        rng.GetBytes(num);
        return Convert.ToBase64String(num);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        TokenValidationParameters? parameters = new()
        {
            ValidateIssuer=false,
            ValidateAudience=false,
            ValidateIssuerSigningKey=true,
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey)),
            ValidateLifetime=false,
        };
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        var principial=handler.ValidateToken(token, parameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken 
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token is not found");
        }
        return principial;
    }   
}
