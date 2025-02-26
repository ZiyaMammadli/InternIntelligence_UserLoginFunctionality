using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserLoginFunctionality.Application.Features.Auth.Rules;
using UserLoginFunctionality.Application.Services;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly ITokenService _tokenService;
    private readonly RefreshTokenRules _refreshTokenRules;
    private readonly UserManager<AppUser> _userManager;

    public RefreshTokenCommandHandler(ITokenService tokenService,RefreshTokenRules refreshTokenRules,UserManager<AppUser> userManager)
    {
        _tokenService = tokenService;
        _refreshTokenRules = refreshTokenRules;
        _userManager = userManager;
    }
    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal? claimsPrincipal= _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        await _refreshTokenRules.EnsurePrincipalExpiredTokenNotFound(claimsPrincipal);

        string? email=claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        AppUser? appUser= await _userManager.FindByEmailAsync(email);
        await _refreshTokenRules.EnsureRereshTokenBelongToUser(appUser.RefreshToken,request.RefreshToken);
        await _refreshTokenRules.EnsureRefreshTokenExpiredTime(appUser.RefreshTokenExpireTime);

        JwtSecurityToken jwtSecurityToken= await _tokenService.GenerateAccessTokenAsync(appUser);
        string accessToken=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        string refreshToken = _tokenService.GenerateRefreshToken();
        appUser.RefreshToken=refreshToken;

        await _userManager.UpdateAsync(appUser);

        RefreshTokenCommandResponse commandResponse = new RefreshTokenCommandResponse()
        {
            AccessToken=accessToken,
            RefreshToken=refreshToken,
        };
        return commandResponse;
    }
}
