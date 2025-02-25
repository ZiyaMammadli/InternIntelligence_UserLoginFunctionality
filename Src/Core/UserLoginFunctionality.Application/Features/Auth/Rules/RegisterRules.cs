using UserLoginFunctionality.Application.Bases;
using UserLoginFunctionality.Application.Exceptions.Auth;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Application.Features.Auth.Rules;

public class RegisterRules:BaseRule
{
    public Task EnsureUserExist(AppUser user)
    {
        if (user is not null) throw new UserAlreadyExistException(400,"User is already exist");
        return Task.CompletedTask;
    }
}
