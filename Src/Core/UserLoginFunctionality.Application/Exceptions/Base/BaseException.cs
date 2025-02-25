namespace UserLoginFunctionality.Application.Exceptions.Base;

public class BaseException:Exception
{
    public int StatusCode { get; set; }
    public BaseException() { }
    public BaseException(string message):base(message) { }
    public BaseException(int statusCode,string message) : base(message)
    {
        StatusCode=statusCode;
    }
}
