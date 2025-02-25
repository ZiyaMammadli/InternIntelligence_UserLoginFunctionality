using System.Security.Claims;
using UserLoginFunctionality.Application.Bases;
using UserLoginFunctionality.Application.Exceptions.Auth;

namespace UserLoginFunctionality.Application.Features.Auth.Rules;

public class RefreshTokenRules : BaseRule
{
    public Task EnsurePrincipalExpiredTokenNotFound(ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal is null) throw new PrincipalExpiredTokenNotFoundException(404, "PrincipalExpiredToken is not found");
        return Task.CompletedTask;
    }
    public Task EnsureRefreshTokenExpiredTime(DateTime? expiredTime)
    {
        if (expiredTime <= DateTime.UtcNow) throw new RefreshTokenExpiredException(400, "RefreshToken has expired");
        return Task.CompletedTask;
    }
}
