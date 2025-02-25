using UserLoginFunctionality.Application.Exceptions.Base;

namespace UserLoginFunctionality.Application.Exceptions.Auth;

public class PrincipalExpiredTokenNotFoundException:BaseException
{
    public PrincipalExpiredTokenNotFoundException() { }
    public PrincipalExpiredTokenNotFoundException(string message) : base(message) { }
    public PrincipalExpiredTokenNotFoundException(int statusCode, string message) : base(statusCode, message) { }
}
