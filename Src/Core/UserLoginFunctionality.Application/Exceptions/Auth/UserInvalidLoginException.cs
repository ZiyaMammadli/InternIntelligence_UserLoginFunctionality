using UserLoginFunctionality.Application.Exceptions.Base;

namespace UserLoginFunctionality.Application.Exceptions.Auth;

public class UserInvalidLoginException:BaseException
{
    public UserInvalidLoginException() { }
    public UserInvalidLoginException(string message) : base(message) { }
    public UserInvalidLoginException(int statusCode, string message) : base(statusCode, message) { }
}
