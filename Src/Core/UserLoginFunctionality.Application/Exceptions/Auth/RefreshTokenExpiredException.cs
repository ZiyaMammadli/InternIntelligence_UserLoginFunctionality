using UserLoginFunctionality.Application.Exceptions.Base;

namespace UserLoginFunctionality.Application.Exceptions.Auth;

public class RefreshTokenExpiredException:BaseException
{
    public RefreshTokenExpiredException() { }
    public RefreshTokenExpiredException(string message) : base(message) { }
    public RefreshTokenExpiredException(int statusCode, string message) : base(statusCode, message) { }
}
