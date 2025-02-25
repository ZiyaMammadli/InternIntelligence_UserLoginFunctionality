using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using UserLoginFunctionality.Application.Features.Auth.Rules;
using UserLoginFunctionality.Application.Services;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly LoginRules _loginRules;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(UserManager<AppUser> userManager,LoginRules loginRules,ITokenService tokenService,IConfiguration configuration)
    {
        _userManager = userManager;
        _loginRules = loginRules;
        _tokenService = tokenService;
        _configuration = configuration;
    }
    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        await _loginRules.EnsureUserExist(user);
        bool checkPassword=await _userManager.CheckPasswordAsync(user,request.Password);
        await _loginRules.EnsureUserLogin(checkPassword);
        var Token=await _tokenService.GenerateAccessTokenAsync(user);
        string accessToken=new JwtSecurityTokenHandler().WriteToken(Token);
        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
        var refreshToken =_tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireTime=DateTime.UtcNow.AddDays(refreshTokenValidityInDays);
        await _userManager.UpdateAsync(user);
        await _userManager.UpdateSecurityStampAsync(user);
        LoginCommandResponse response = new LoginCommandResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Expiration=Token.ValidTo,
        };
        return response;
    }
}
