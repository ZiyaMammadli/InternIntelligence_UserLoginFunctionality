using UserLoginFunctionality.Application.Bases;
using UserLoginFunctionality.Application.Exceptions.Auth;
using UserLoginFunctionality.Domian.Entities;

namespace UserLoginFunctionality.Application.Features.Auth.Rules;

public class LoginRules:BaseRule
{
    public Task EnsureUserExist(AppUser user)
    {
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        return Task.CompletedTask;
    }
    public Task EnsureUserLogin(bool checkPassword)
    {
        if(!checkPassword) throw new UserInvalidLoginException(400,"Password or Email is incorrect");
        return Task.CompletedTask;
    }
}
