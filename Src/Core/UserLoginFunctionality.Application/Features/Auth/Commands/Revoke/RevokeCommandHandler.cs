using MediatR;
using Microsoft.AspNetCore.Identity;
using UserLoginFunctionality.Application.Features.Auth.Rules;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.Revoke;

public class RevokeCommandHandler : IRequestHandler<RevokeCommandRequest, Unit>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly LoginRules _loginRules;

    public RevokeCommandHandler(UserManager<AppUser> userManager,LoginRules loginRules)
    {
        _userManager = userManager;
        _loginRules = loginRules;
    }
    public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
    {
        var user=await _userManager.FindByEmailAsync(request.Email);
        await _loginRules.EnsureUserNotFound(user);
        user.RefreshToken = "null";
        await _userManager.UpdateAsync(user);
        return Unit.Value;
    }
}
