using MediatR;
using Microsoft.AspNetCore.Identity;
using UserLoginFunctionality.Application.Features.Auth.Rules;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, Unit>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RegisterRules _registerRules;
    private readonly RoleManager<Role> _roleManager;

    public RegisterCommandHandler(UserManager<AppUser> userManager, RegisterRules registerRules, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _registerRules = registerRules;
        _roleManager = roleManager;
    }
    public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        await _registerRules.EnsureUserExist(user);
        AppUser appUser = new()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
            RefreshToken = "null"
        };
        var result = await _userManager.CreateAsync(appUser, request.Password);
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("member"))
            {
                Role role = new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "member",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                };
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "member");
        }
        return Unit.Value;

    }
}
