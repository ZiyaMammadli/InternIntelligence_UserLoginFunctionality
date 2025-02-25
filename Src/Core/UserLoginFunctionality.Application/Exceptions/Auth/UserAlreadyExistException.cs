using UserLoginFunctionality.Application.Exceptions.Base;

namespace UserLoginFunctionality.Application.Exceptions.Auth;

public class UserAlreadyExistException:BaseException
{
    public UserAlreadyExistException() { }
    public UserAlreadyExistException(string message) : base(message) { }
    public UserAlreadyExistException(int statusCode, string message) : base(statusCode, message) { }
}
