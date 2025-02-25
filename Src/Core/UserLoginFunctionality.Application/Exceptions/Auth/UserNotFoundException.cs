using UserLoginFunctionality.Application.Exceptions.Base;

namespace UserLoginFunctionality.Application.Exceptions.Auth;

public class UserNotFoundException:BaseException
{
    public UserNotFoundException() { }
    public UserNotFoundException(string message):base(message) { }
    public UserNotFoundException(int statusCode,string message):base(statusCode,message) { }
}
