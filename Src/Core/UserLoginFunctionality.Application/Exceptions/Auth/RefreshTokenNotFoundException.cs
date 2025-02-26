using UserLoginFunctionality.Application.Exceptions.Base;

namespace UserLoginFunctionality.Application.Exceptions.Auth;

public class RefreshTokenNotFoundException:BaseException
{
    public RefreshTokenNotFoundException() { }
    public RefreshTokenNotFoundException(string message) : base(message) { }
    public RefreshTokenNotFoundException(int statusCode, string message) : base(statusCode, message) { }
}
